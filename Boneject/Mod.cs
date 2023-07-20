using System;
using Boneject.Context;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Boneject
{
    public class Mod : MelonMod
    {
        private KernelConfiguration _kernel = null!;
        private SceneContext _appContext = null!;

        private INinjectSettings _ninjectSettings = new NinjectSettings
        {
            InjectAttribute = typeof(InjectAttribute),
            CachePruningInterval = TimeSpan.FromSeconds(30d),
            DefaultScopeCallback = StandardScopeCallbacks.Transient,
            LoadExtensions = false,
            UseReflectionBasedInjection = false,
            InjectNonPublic = true,
            InjectParentPrivateProperties = false,
            ActivationCacheDisabled = false,
            AllowNullInjection = true
        };

        public override void OnInitializeMelon()
        {
            _kernel = new KernelConfiguration(_ninjectSettings);
        }

        // Start()
        public override void OnLateInitializeMelon()
        {
            var appContextObject = new GameObject("BonejectApplicationContext");
            appContextObject.SetActive(false);
            
            _appContext = appContextObject.AddComponent<SceneContext>();
            _appContext.Kernel = _kernel;
            
            Object.DontDestroyOnLoad(appContextObject);
            appContextObject.SetActive(true);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            var sceneContextObject = new GameObject("BonejectSceneContext");
            sceneContextObject.SetActive(false);
            
            var sceneContext = sceneContextObject.AddComponent<SceneContext>();
            sceneContext.Kernel = _kernel;
            
            sceneContextObject.SetActive(true);
        }

        public override void OnApplicationQuit()
        {
            Object.Destroy(_appContext);
        }
    }
}
