using System;
using System.Collections.Generic;
using System.Linq;
using Boneject.Ninject.Extensions;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using UnityEngine.SceneManagement;

namespace Boneject.Ninject
{
    internal class BonejectManager
    {
        private readonly NinjectSettings _ninjectSettings = new()
        {
            InjectAttribute = typeof(InjectAttribute),
            CachePruningInterval = TimeSpan.FromSeconds(30.0),
            DefaultScopeCallback = StandardScopeCallbacks.Transient,
            LoadExtensions = true,
            ExtensionSearchPatterns = new[] // Just in case someone decides to use these in the future
            {
                "Ninject.Extensions.*.dll",
                "Ninject.Web*.dll"
            },
            UseReflectionBasedInjection = false,
            ActivationCacheDisabled = false,
            AllowNullInjection = false,
            MethodInjection = true,
            PropertyInjection = true,
            ThrowOnGetServiceNotFound = false
        };

        private BonejectKernel? _kernel;
        private readonly HashSet<Bonejector> _bonejectors = new();

        internal BonejectKernel Kernel
        {
            get
            {
                if (_kernel is not null) return _kernel;

                _kernel = new BonejectKernel(_ninjectSettings);
                _kernel.Load(new AppModule(this)); // always ensure that the app module exists

                return _kernel;
            }
        }

        internal Dictionary<string, HashSet<IBinding>> SceneBindings { get; } = new();

        internal Dictionary<string, HashSet<INinjectModule>> SceneModules { get; } = new();

        public void Add(Bonejector bonejector)
        {
            _bonejectors.Add(bonejector);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void Enable()
        {
            // TODO: May add some stuff here later if ML ever supports enabling and disabling mods.
        }

        public void LoadForContext(INinjectModule baseModule, string? overrideSceneName = null)
        {
            foreach (Bonejector? bonejector in _bonejectors)
            {
                foreach (LoadSet set in bonejector.LoadSets)
                {
                    if (!set.LoadFilter.ShouldLoad(baseModule)) continue;

                    MelonLogger.Msg($"Loading {set.ModuleType.FullName} into {baseModule.GetType().FullName}");
                    Kernel.Load(set.ModuleType, set.InitialParameters);
                }

                foreach (LoadInstruction instruction in bonejector.LoadInstructions)
                {
                    if (instruction.BaseModule == baseModule.GetType())
                        instruction.OnLoad(Kernel);
                }
            }

            string? sceneName = overrideSceneName ?? SceneManager.GetActiveScene().name;

            if (!SceneBindings.Keys.Contains(sceneName))
                SceneBindings.Add(sceneName, new HashSet<IBinding>());
            if (!SceneModules.Keys.Contains(sceneName))
                SceneModules.Add(sceneName, new HashSet<INinjectModule>());

            // expression-generated lambda for accessing non-public member; faster than reflection
            Func<KernelBase, Dictionary<Type, ICollection<IBinding>>> bindingsAccessor =
                AccessUtils.GetFieldAccessor<KernelBase, Dictionary<Type, ICollection<IBinding>>>("bindings");
            Dictionary<Type, ICollection<IBinding>>? bindings = bindingsAccessor(Kernel);
            // ensure all bindings are registered under the scene name after they have all been loaded
            foreach (IBinding? binding in bindings.SelectMany(bindingCollection => bindingCollection.Value))
                SceneBindings[sceneName].Add(binding);

            IEnumerable<INinjectModule> modules = Kernel.GetModules();
            foreach (INinjectModule? module in modules)
                SceneModules[sceneName].Add(module);
        }

        public void Disable()
        {
            _kernel?.Dispose(true);
            _kernel = null;
        }
    }
}
