using Boneject.ModuleLoaders;
using HarmonyLib;
using MelonLoader;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(BonelabGameControl))]
[HarmonyPatch(nameof(BonelabGameControl.Start))]
internal static class GameControlPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(BonelabGameControl __instance)
    {
        MelonLogger.Msg($"Loading modules for location: {InstallLocation.Game}");
        var moduleLoader = new GameModuleLoader();
        moduleLoader.Kernel?.Bind<BonelabGameControl>().ToConstant(__instance).InSingletonScope();
        moduleLoader.BeginLoad();
    }
}