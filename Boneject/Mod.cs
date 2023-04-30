﻿using System.Reflection;
using Boneject.MelonLoader;
using Boneject.Ninject;
using MelonLoader;
using UnityEngine;

namespace Boneject;

internal static class BuildInfo
{
    public const string id = "dev.millzy.Boneject";
    public const string name = "Boneject";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "1.0.0.0";
    public const string semanticVersion = "1.0.0-pre.1";
    public const string downloadLink = "https://github.com/MillzyDev/Boneject/releases/latest/download/Boneject.zip";
}

internal class Mod : MelonMod
{
    private HarmonyLib.Harmony _harmony = null!;
    private BonejectManager _bonejectManager = null!;

    public Mod() => Instance = this;

    private static Mod Instance { get; set; } = null!;
    internal static BonejectManager BonejectManager => Instance._bonejectManager;

    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
    public new HarmonyLib.Harmony HarmonyInstance => _harmony; // Hide default harmony field.

    public override void OnInitializeMelon()
    {
        _harmony = new HarmonyLib.Harmony(BuildInfo.id);
        _bonejectManager = new BonejectManager();

        ModInitInjector.AddInjector(typeof(Bonejector), ConstructBonejector);
    }

    public override void OnLateInitializeMelon()
    {
        _harmony.PatchAll();
        _bonejectManager.Enable();

        GameObject gameObject = new("Boneject Context Handler");
        gameObject.SetActive(false);
        var contextHandler = gameObject.AddComponent<BonejectContextHandler>();
        contextHandler.BonejectManager = _bonejectManager;
        gameObject.SetActive(true);

        _ = BonejectManager.Kernel; // Force the kernel to init.
    }

    public override void OnDeinitializeMelon()
    {
        _bonejectManager.Disable();
        _harmony.UnpatchSelf();
    }

    private object ConstructBonejector(object? previous, ParameterInfo _, MelonInfoAttribute info)
    {
        if (previous is not null)
            return previous;
        
        Bonejector bonejector = new(info);
        _bonejectManager.Add(bonejector);
        return bonejector;
    }
}