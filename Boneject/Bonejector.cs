using System;
using System.Collections.Generic;
using Boneject.Filters;
using Boneject.Loading;
using Boneject.Modules;
using MelonLoader;
using Ninject.Modules;

namespace Boneject
{
    public class Bonejector
    {
        private readonly HashSet<LoadSet> _loadSets = new();
        private readonly HashSet<SceneLoadSet> _sceneLoadSets = new();

        internal protected Bonejector(MelonInfoAttribute melonInfo)
        {
            MelonInfo = melonInfo;
        }

        public IEnumerable<LoadSet> LoadSets
        {
            get => _loadSets;
        }

        public IEnumerable<SceneLoadSet> SceneLoadSets
        {
            get => _sceneLoadSets;
        }

        public MelonInfoAttribute MelonInfo { get; }

        public static Bonejector Get(MelonInfoAttribute info)
        {
            var bonejector = new Bonejector(info);
            // TODO: Register in BonejectManager
            return bonejector;
        }

        public void Load<TModule>(Location location, params object[] parameters)
            where TModule : INinjectModule
        {
            IEnumerable<Type> moduleTypes = ModulesForLocation(location);
            ILoadFilter filter = new MultiTypedLoadFilter(moduleTypes);
            _loadSets.Add(
                new LoadSet(
                    moduleType: typeof(TModule),
                    loadFilter: filter,
                    initialArguments: parameters.Length != 0 ? parameters : null
                )
            );
        }

        public void Load<TCustomModule, TBaseModule>(params object[] parameters)
            where TCustomModule : INinjectModule where TBaseModule : INinjectModule
        {
            ILoadFilter filter = new TypedLoadFilter(typeof(TBaseModule));
            _loadSets.Add(
                new LoadSet(
                    moduleType: typeof(TCustomModule),
                    loadFilter: filter,
                    initialArguments: parameters.Length != 0 ? parameters : null
                )
            );
        }

        public void Load<TModule>(string sceneName, params object[] parameters)
        {
            ISceneLoadFilter filter = new SceneLoadFilter(sceneName);
            _sceneLoadSets.Add(
                new SceneLoadSet(
                    moduleType: typeof(TModule),
                    loadFilter: filter,
                    initialArguments: parameters
                )
            );
        }

        public void Load<TModule>(string[] sceneNames, params object[] parameters)
        {
            ISceneLoadFilter filter = new MultiSceneLoadFilter(sceneNames);
            _sceneLoadSets.Add(
                new SceneLoadSet(
                    moduleType: typeof(TModule),
                    loadFilter: filter,
                    initialArguments: parameters
                )
            );
        }

        private IEnumerable<Type> ModulesForLocation(Location location)
        {
            HashSet<Type> moduleTypes = new();
            
            if (location.HasFlag(Location.App))
                moduleTypes.Add(typeof(AppModule));
            if (location.HasFlag(Location.Bootstrapper))
                moduleTypes.Add(typeof(BootstrapperModule));
            if (location.HasFlag(Location.Loading))
                moduleTypes.Add(typeof(LoadingModule));
            if (location.HasFlag(Location.Player))
                moduleTypes.Add(typeof(PlayerModule));
            if (location.HasFlag(Location.Startup))
                moduleTypes.Add(typeof(StartupModule));
            if (location.HasFlag(Location.VoidG114Menu))
                moduleTypes.Add(typeof(VoidG114MenuModule));
            
            return moduleTypes;
        }
    }
}
