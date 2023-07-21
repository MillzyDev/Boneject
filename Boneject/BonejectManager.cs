using System;
using System.Collections.Generic;
using System.Linq;
using Boneject.Context;
using Boneject.Loading;
using Boneject.Modules;
using MelonLoader;
using Ninject;
using Ninject.Modules;

namespace Boneject
{
    public class BonejectManager
    {
        private readonly Dictionary<string, Type> _baseModules = new();
        private readonly Type _appBaseModule = typeof(AppModule);
        private readonly HashSet<BonejectorDatum> _bonejectors = new();

        internal void MelonRegistered(MelonBase melon)
        {
            if (melon.MelonTypeName != "Mod") return;

            BonejectorDatum? datum =
                _bonejectors.FirstOrDefault(datum => Equals(datum.Bonejector.MelonInfo, melon.Info));
            if (datum is not null)
                datum.Enabled = true;
        }
        
        internal void MelonUnregistered(MelonBase melon)
        {
            if (melon.MelonTypeName != "Mod") return;

            BonejectorDatum? datum =
                _bonejectors.FirstOrDefault(datum => Equals(datum.Bonejector.MelonInfo, melon.Info));
            if (datum is not null)
                datum.Enabled = false;
        }

        public void Enable()
        {
            foreach (BonejectorDatum? datum in _bonejectors)
            {
                MelonInfoAttribute info = datum.Bonejector.MelonInfo;
                datum.Enabled = MelonBase.FindMelon(info.Name, info.Author) != null;
            }
        }

        public void Add(Bonejector bonejector)
        {
            _bonejectors.Add(new BonejectorDatum(bonejector));
        }

        public void RegisterBaseModule<TBaseModule>(string sceneName)
        {
            _baseModules.Add(sceneName, typeof(TBaseModule));
        }

        public void LoadBaseModule(ref SceneContext context)
        {
            if (!_baseModules.TryGetValue(context.ContractName, out Type moduleType))
                return;

            var module = Activator.CreateInstance(moduleType) as INinjectModule;
            context.Kernel.Load(module);
            context.BaseModule = module!;
        }

        public void LoadAppModule(ref SceneContext appContext)
        {
            var module = Activator.CreateInstance(_appBaseModule) as INinjectModule;
            appContext.Kernel.Load(module);
            appContext.BaseModule = module!;
        }

        public void LoadForContext(SceneContext context)
        {
            foreach (BonejectorDatum? datum in _bonejectors)
            {
                if (!datum.Enabled)
                    continue;

                Bonejector bonejector = datum.Bonejector;

                foreach (SceneLoadSet set in bonejector.SceneLoadSets)
                {
                    if (set.LoadFilter.ShouldLoad(context.gameObject.scene.name))
                    {
#if DEBUG
                        MelonLogger.Msg($"Loading: {set.ModuleType.FullName} into context for {context.gameObject.scene.name}");
#endif
                        if (!typeof(INinjectModule).IsAssignableFrom(set.ModuleType))
                            MelonLogger.Error($"Unable to load {set.ModuleType.FullName}; is not assignable to INinjectModule.");

                        var module =
                            Activator.CreateInstance(set.ModuleType, set.InitialArguments) as INinjectModule;
                        
                        context.Kernel.Load(module);
                    }
                }

                foreach (LoadSet? set in bonejector.LoadSets)
                {
                    if (set.LoadFilter.ShouldLoad(context.BaseModule))
                    {
#if DEBUG
                        MelonLogger.Msg($"Loading: {set.ModuleType.FullName} into {context.BaseModule.GetType().FullName}");
#endif
                        if (!typeof(INinjectModule).IsAssignableFrom(set.ModuleType))
                            MelonLogger.Error($"Unable to load {set.ModuleType.FullName}; is not assignable to INinjectModule.");

                        var module =
                            Activator.CreateInstance(set.ModuleType, set.InitialArguments) as INinjectModule;
                        
                        context.Kernel.Load(module);
                    }
                }
            }
        }
    }
}
