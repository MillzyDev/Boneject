using System;
using System.Collections.Generic;
using MelonLoader;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Boneject.Ninject
{
    [RegisterTypeInIl2Cpp]
    internal class BonejectContextHandler : MonoBehaviour
    {
        private BonejectManager? _bonejectManager;

        public BonejectContextHandler(IntPtr ptr) : base(ptr)
        {
        }

        [HideFromIl2Cpp]
        internal BonejectManager BonejectManager
        {
            [HideFromIl2Cpp]
            get => _bonejectManager!;
            [HideFromIl2Cpp]
            set => _bonejectManager ??= value; // only set if not already set
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }

        private void Start() // Ensure _bonejectManager has a value;
        {
            SceneManager.sceneUnloaded += DelegateSupport.ConvertDelegate<UnityAction<Scene>>(OnSceneUnloaded);
        }

        private void OnSceneUnloaded(Scene scene)
        {
            UnloadNonAppBindings(scene.name);
        }

        [HideFromIl2Cpp]
        private void UnloadNonAppBindings(string sceneName)
        {
            BonejectKernel? kernel = BonejectManager.Kernel;

            // Unregister modules
            if (BonejectManager.SceneModules.TryGetValue(sceneName, out HashSet<INinjectModule>? modules))
            {
                foreach (INinjectModule? module in modules)
                    kernel.Unload(module.Name);
            }

            // Remove bindings
            if (!BonejectManager.SceneBindings.TryGetValue(sceneName, out HashSet<IBinding>? bindings)) return;
            foreach (IBinding? binding in bindings)
                kernel.RemoveBinding(binding);
        }
    }
}
