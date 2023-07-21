using System;
using System.Linq;
using Boneject.Context;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using SLZ.Rig;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Boneject
{
    public class Mod : MelonMod
    {
        private BonejectManager? _bonejectManager;
        private KernelConfiguration? _kernel;
        private SceneContext _appContext = null!;

        private readonly INinjectSettings _ninjectSettings = new NinjectSettings
        {
            InjectAttribute = typeof(InjectAttribute),
            CachePruningInterval = TimeSpan.FromSeconds(30.0),
            DefaultScopeCallback = StandardScopeCallbacks.Transient,
            LoadExtensions = false,
            UseReflectionBasedInjection = false,
            InjectNonPublic = true,
            InjectParentPrivateProperties = false,
            ActivationCacheDisabled = false,
            AllowNullInjection = true
        };

        public override void OnEarlyInitializeMelon()
        {
            _bonejectManager ??= new BonejectManager();
            _kernel ??= new KernelConfiguration(_ninjectSettings);
        }

        public override void OnInitializeMelon()
        {
            OnMelonRegistered.Subscribe(_bonejectManager!.MelonRegistered);
            OnMelonUnregistered.Subscribe(_bonejectManager.MelonUnregistered);
            _bonejectManager.Enable();

            Bonejector bonejector = new BonejectorBuilder(Info).Build();
            _bonejectManager.Add(bonejector);
        }

        // Start()
        public override void OnLateInitializeMelon()
        {
            var appContextObject = new GameObject("BonejectApplicationContext");
            appContextObject.SetActive(false);
            
            _appContext = appContextObject.AddComponent<SceneContext>();
            _appContext.Kernel = _kernel!;
            _appContext.BonejectManager = _bonejectManager!;
            _bonejectManager!.LoadAppModule(ref _appContext);

            Object.DontDestroyOnLoad(appContextObject);
            appContextObject.SetActive(true);
        }

        public override void OnDeinitializeMelon()
        {
            OnMelonRegistered.Unsubscribe(_bonejectManager!.MelonRegistered);
            OnMelonUnregistered.Unsubscribe(_bonejectManager.MelonUnregistered);
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            var sceneContextObject = new GameObject("BonejectSceneContext");
            sceneContextObject.SetActive(false);
            
            var sceneContext = sceneContextObject.AddComponent<SceneContext>();
            sceneContext.Kernel = _kernel!;
            sceneContext.BonejectManager = _bonejectManager!;
            _bonejectManager!.LoadBaseModule(ref sceneContext);
            
            sceneContextObject.SetActive(true);
            
            LoggerInstance.Msg($"Loaded into {sceneName}");
            LoggerInstance.Msg($"RigManager is exists? {Resources.FindObjectsOfTypeAll<RigManager>().Any()}");
        }

        public override void OnApplicationQuit()
        {
            Object.Destroy(_appContext);
        }
    }
}
