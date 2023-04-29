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
        get => _bonejectManager!;
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

    private void OnSceneUnloaded(Scene _) => UnloadNonAppBindings();

    [HideFromIl2Cpp]
    private void UnloadNonAppBindings()
    {
        var kernel = BonejectManager.Kernel;
        
        // Unregister modules
        IEnumerable<INinjectModule> modules =
            kernel.GetModules().Where(module => !BonejectManager.PreservedModules.Contains(module));
        foreach (var module in modules)
            kernel.Unload(module.Name);

        // Remove bindings
        var bindings = BonejectManager.BindingCache.Values.SelectMany(value => value)
            .Where(binding => !BonejectManager.PreservedBindings.Contains(binding));
        foreach (var binding in bindings)
            kernel.RemoveBinding(binding);
    }
}