using Boneject.ModuleLoaders;
using HarmonyLib;
using SLZ.Bonelab;
using MelonLoader;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_startup))]
[HarmonyPatch(nameof(GameControl_startup.Start))]
// ReSharper disable once InconsistentNaming
internal static class GameControl_startupPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_startup __instance)
    {
        MelonLogger.Msg($"Loading modules for location: {Context.MenuStartup}");
        var moduleLoader = new MenuStartupModuleLoader();
        moduleLoader.Kernel?.Bind<GameControl_startup>().ToConstant(__instance).InSingletonScope();
        moduleLoader.BeginLoad();
    }
}