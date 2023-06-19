using System;
using System.Collections.Generic;
using Boneject.Ninject.Filters;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject.Modules;

namespace Boneject.Ninject
{
    public class Bonejector
    {
        private readonly HashSet<LoadSet> _loadSets = new();
        private readonly HashSet<LoadInstruction> _loadInstructions = new();

        internal Bonejector(MelonInfoAttribute info)
        {
            ModInfo = info;
        }

        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        internal MelonInfoAttribute ModInfo { get; }

        internal IEnumerable<LoadSet> LoadSets
        {
            get => _loadSets;
        }

        internal IEnumerable<LoadInstruction> LoadInstructions
        {
            get => _loadInstructions;
        }

        /// <summary>
        /// Loads a custom module to a location with a base module(s).
        /// </summary>
        /// <param name="context">The context/location to load it to.</param>
        /// <param name="parameters">The parameters your module will be constructed with.</param>
        /// <typeparam name="T">The type of your custom module.</typeparam>
        public void Load<T>(Context context, params object[] parameters) where T : INinjectModule
        {
            IEnumerable<Type> moduleTypes = ModuleForContext(context);
            // only load at the correct modules, correct module types are obtained above
            ILoadFilter filter = new MultiTypedLoadFilter(moduleTypes);
            _loadSets.Add(new LoadSet(typeof(T), filter, parameters.Length != 0 ? parameters : null));
        }

        /// <summary>
        /// Load bindings to a custom location with a backing module(s).
        /// </summary>
        /// <param name="context">The context/location to install it to.</param>
        /// <param name="loadCallback">The callback used to load custom bindings into the kernel.</param>
        public void Load(Context context, Action<BonejectKernel> loadCallback)
        {
            foreach (Type? module in ModuleForContext(context))
                _loadInstructions.Add(new LoadInstruction(module, loadCallback));
        }

        private static IEnumerable<Type> ModuleForContext(Context context)
        {
            HashSet<Type> moduleTypes = new();

            if (context.HasFlag(Context.App))
                moduleTypes.Add(typeof(AppModule));
            if (context.HasFlag(Context.Loading))
                moduleTypes.Add(typeof(LoadingModule));
            if (context.HasFlag(Context.Campaign))
                moduleTypes.Add(typeof(CampaignModule));
            if (context.HasFlag(Context.Hub))
                moduleTypes.Add(typeof(HubModule));
            if (context.HasFlag(Context.Startup))
                moduleTypes.Add(typeof(StartupModule));
#pragma warning disable CS0612
            if (context.HasFlag(Context.VoidG114))
#pragma warning restore CS0612
                moduleTypes.Add(typeof(VoidG114Module));
            if (context.HasFlag(Context.Player))
                moduleTypes.Add(typeof(PlayerModule));
            if (context.HasFlag(Context.VoidG114Menu))
                moduleTypes.Add(typeof(VoidG114MenuModule));

            return moduleTypes;
        }
    }
}
