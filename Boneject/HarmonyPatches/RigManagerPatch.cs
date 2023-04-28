using Boneject.Ninject.Modules;
using HarmonyLib;
using Ninject;
using SLZ.Rig;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(RigManager))]
[HarmonyPatch(nameof(RigManager.Start))]
public static class RigManagerPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void PostFix(RigManager __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var baseModule = new PlayerModule(bonejectManager, __instance);
        kernel.Load(baseModule);
    }
}