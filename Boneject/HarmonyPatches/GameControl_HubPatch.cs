using Boneject.ModuleLoaders;
using HarmonyLib;
using MelonLoader;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_Hub))]
[HarmonyPatch(nameof(GameControl_Hub.Start))]
// ReSharper disable once InconsistentNaming
internal static class GameControl_HubPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    private static void Postfix(GameControl_Hub __instance)
    {
        MelonLogger.Msg($"Loading modules for context: {Context.Game}");
        var moduleLoader = new HubModuleLoader();
        moduleLoader.Kernel?.Bind<GameControl_Hub>().ToConstant(__instance).InSingletonScope();
        moduleLoader.BeginLoad();
    }
}