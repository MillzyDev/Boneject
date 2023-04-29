using Boneject.Ninject.Modules;
using HarmonyLib;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_Hub))]
[HarmonyPatch(nameof(GameControl_Hub.Start))]
public static class GameControl_HubPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_Hub __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var rigManager = __instance.rm;
        var controlPlayer = __instance.controlPlayer;
        var gauntletElevator = __instance.gauntletElevator;
        var inventorySaveFilter = __instance.inventorySaveFilter;

        var baseModule = new HubModule(bonejectManager, __instance, rigManager, controlPlayer, gauntletElevator,
            inventorySaveFilter);
        kernel.Load(baseModule);
    }
}