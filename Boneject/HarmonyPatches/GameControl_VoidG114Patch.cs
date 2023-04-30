using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_VoidG114))]
[HarmonyPatch(nameof(GameControl_VoidG114.Start))]
public static class GameControl_VoidG114Patch
{
    [HarmonyPostfix]
    // ReSharper disable once UnusedMember.Local
    // ReSharper disable once InconsistentNaming
    private static void Postfix(GameControl_VoidG114 __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var rigManager = __instance.rm;
        var bodyVitals = __instance.ctrl_bodyVitals;

        var baseModule = new VoidG114Module(bonejectManager, __instance, rigManager, bodyVitals);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("VoidG114 context loaded.");
    }
}