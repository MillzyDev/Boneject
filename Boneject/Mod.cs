using Boneject.Context;
using MelonLoader;
using Ninject;
using UnityEngine;

namespace Boneject
{
    public class Mod : MelonMod
    {
        private SceneContext _appContext = null!;
        
        // Start()
        public override void OnLateInitializeMelon()
        {
            var appContextObject = new GameObject("BonejectApplicationContext");
            appContextObject.SetActive(false);
            
            _appContext = appContextObject.AddComponent<SceneContext>();
            
            Object.DontDestroyOnLoad(appContextObject);
            appContextObject.SetActive(true);
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            var sceneContextObject = new GameObject("BonejectSceneContext");
            sceneContextObject.SetActive(false);
            
            var sceneContext = sceneContextObject.AddComponent<SceneContext>();
            sceneContext.Kernel = _appContext.Kernel;
            
            sceneContextObject.SetActive(true);
        }

        public override void OnApplicationQuit()
        {
            Object.Destroy(_appContext);
        }
    }
}
