using System.Reflection;
using Boneject.MelonLoader;
using Boneject.Ninject;
using MelonLoader;

namespace Boneject;

public static class BuildInfo
{
    public const string id = "dev.millzy.boneject";
    public const string name = "Boneject";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "1.0.0";
    public const string downloadLink = null!;
}

public class Mod : MelonMod
{
    private HarmonyLib.Harmony _harmony = null!;
    private BonejectManager _bonejectManager = null!;

    public Mod() => Instance = this;

    public static Mod Instance { get; private set; } = null!;
    public static BonejectManager BonejectManager => Instance._bonejectManager;

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
    }

    public override void OnDeinitializeMelon()
    {
        _bonejectManager.Disable();
        _harmony.UnpatchSelf();
    }

    public override void OnSceneWasLoaded(int _, string __)
    {
        _bonejectManager.ContextChanged();
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