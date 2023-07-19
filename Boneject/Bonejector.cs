using System;
using System.Collections.Generic;
using Boneject.Loading;
using Boneject.Modules;
using MelonLoader;
using Ninject.Modules;

namespace Boneject
{
    public class Bonejector
    {
        private Bonejector(MelonInfoAttribute melonInfo)
        {
            MelonInfo = melonInfo;
        }

        public HashSet<LoadSet> LoadSets { get; } = new();

        public HashSet<SceneLoadSet> SceneLoadSets { get; } = new();

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
            
        }

        public void Load<TCustomModule, TBaseModule>(params object[] parameters)
            where TCustomModule : INinjectModule where TBaseModule : INinjectModule
        {
            
        }

        public void Load<TModule>(string sceneName, params object[] parameters)
        {
            
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
