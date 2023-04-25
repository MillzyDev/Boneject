using Boneject.ModuleLoaders;
using HarmonyLib;
using SLZ.Bonelab;
using MelonLoader;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_VoidG114))]
[HarmonyPatch(nameof(GameControl_VoidG114.Start))]
// ReSharper disable once InconsistentNaming
internal static class GameControl_MenuVoidG114Patch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_MenuVoidG114 __instance)
    {
        MelonLogger.Msg($"Loading modules for location: {Context.MenuVoidG114}");
        var moduleLoader = new MenuVoidG114ModuleLoader();
        moduleLoader.Kernel?.Bind<GameControl_MenuVoidG114>().ToConstant(__instance).InSingletonScope();
        moduleLoader.BeginLoad();
    }
}