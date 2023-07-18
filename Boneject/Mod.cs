using Boneject.Context;
using MelonLoader;
using UnityEngine;

namespace Boneject
{
    public class Mod : MelonMod
    {
        
        
        // Start()
        public override void OnLateInitializeMelon()
        {
            var appContextObject = new GameObject("BonejectApplicationContext");
            appContextObject.SetActive(false);
            
            var appContext = appContextObject.AddComponent<SceneContext>();
            
            Object.DontDestroyOnLoad(appContextObject);
            appContextObject.SetActive(true);
            
            
        }

        public override void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            var sceneContextObject = new GameObject("BonejectSceneContext");
            sceneContextObject.SetActive(false);
            
            var sceneContext = sceneContextObject.AddComponent<SceneContext>();
            
            sceneContextObject.SetActive(true);
        }
    }
}
