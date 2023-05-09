using System.Collections.Generic;
using System;
using System.Linq;
using MelonLoader;
using Ninject.Modules;
using UnhollowerBaseLib.Attributes;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Boneject.Ninject;

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
        set => _bonejectManager ??= value;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Persist between scenes
    }

    private void Start() // Ensure _bonejectManager has a value;
    {
        SceneManager.sceneUnloaded += DelegateSupport.ConvertDelegate<UnityAction<Scene>>(OnSceneUnloaded);
    }

    private void OnSceneUnloaded(Scene scene) => UnloadNonAppBindings(scene.name);

    [HideFromIl2Cpp]
    private void UnloadNonAppBindings(string sceneName)
    {
        var kernel = BonejectManager.Kernel;
        
        // Unregister modules
        var modules = BonejectManager.SceneModules[sceneName];
        foreach (var module in modules)
        {
            kernel.Unload(module.Name);
        }

        // Remove bindings
        var bindings = BonejectManager.SceneBindings[sceneName];
        foreach (var binding in bindings)
        {
            kernel.RemoveBinding(binding);
        }
    }
}