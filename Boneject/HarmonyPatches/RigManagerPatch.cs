using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
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
    private static void Postfix(RigManager __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var baseModule = new PlayerModule(bonejectManager, __instance);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("Player context loaded.");
    }
}