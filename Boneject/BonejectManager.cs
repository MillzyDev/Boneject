using System;
using System.Collections.Generic;
using System.Linq;
using Boneject.Extensions;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Boneject
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
                //_kernel.Load(new AppModule(this)); 
                // always ensure that the app module exists
                var appContextObject = new GameObject("Boneject App Context");
                Object.DontDestroyOnLoad(appContextObject);
                _kernel.BindMonoBehaviourOnGameObject<AppContext>(appContextObject);

                return _kernel;
            }
        }

        internal Dictionary<int, HashSet<Type>> SceneBindings { get; } = new();

        internal Dictionary<int, HashSet<Type>> SceneModules { get; } = new();

        public void Add(Bonejector bonejector)
        {
            _bonejectors.Add(bonejector);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Global
        public void Enable()
        {
            // TODO: May add some stuff here later if ML ever supports enabling and disabling mods.
        }

        public void LoadForContext(INinjectModule baseModule, int hostId)
        {
            if (!SceneBindings.Keys.Contains(hostId))
                SceneBindings.Add(hostId, new HashSet<Type>());
            if (!SceneModules.Keys.Contains(hostId))
                SceneModules.Add(hostId, new HashSet<Type>());
            
            if (!SceneModules[hostId].Contains(baseModule.GetType()))
                SceneModules[hostId].Add(baseModule.GetType());
            
            // expression-generated lambda for accessing non-public member; faster than reflection
            Func<KernelBase, Dictionary<Type, ICollection<IBinding>>> bindingsAccessor =
                AccessUtils.GetFieldAccessor<KernelBase, Dictionary<Type, ICollection<IBinding>>>("bindings");
            var oldBindings = new Dictionary<Type, ICollection<IBinding>>(bindingsAccessor(Kernel));
            
            foreach (Bonejector? bonejector in _bonejectors)
            {
                foreach (LoadSet set in bonejector.LoadSets)
                {
                    if (!set.LoadFilter.ShouldLoad(baseModule)) continue;

                    MelonLogger.Msg($"Loading {set.ModuleType.FullName} into {baseModule.GetType().FullName}");
                    INinjectModule module = Kernel.Load(set.ModuleType, set.InitialParameters);
                    SceneModules[hostId].Add(module.GetType());
                }

                foreach (LoadInstruction instruction in bonejector.LoadInstructions)
                {
                    if (instruction.BaseModule == baseModule.GetType())
                        instruction.OnLoad(Kernel);
                }
            }

            // Only add the new bindings, done by checking what wasn't in the list of bindings cloned at the beginning.
            var currentBindings = new Dictionary<Type, ICollection<IBinding>>(bindingsAccessor(Kernel));
            foreach (Type? binding in currentBindings.Keys.Where(type => !oldBindings.ContainsKey(type)))
            {
                SceneBindings[hostId].Add(binding);
            }
        }

        public void UnloadForContext(int hostId)
        {
            // Remove modules
            if (SceneModules.TryGetValue(hostId, out HashSet<Type>? modules))
            {
                foreach (Type module in modules)
                {
                    Kernel.Unload(module.FullName!);
                }
            }

            // Remove bindings
            if (!SceneBindings.TryGetValue(hostId, out HashSet<Type>? bindings)) return;
            foreach (Type? binding in bindings)
                Kernel.Unbind(binding);
        }

        public void Disable()
        {
            _kernel?.Dispose(true);
            _kernel = null;
        }
    }
}
