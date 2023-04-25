using Boneject.ModuleLoaders;
using HarmonyLib;
using MelonLoader;
using SLZ.UI;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(LoadingScene))]
[HarmonyPatch(nameof(LoadingScene.Start))]
internal static class LoadingScenePatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(LoadingScene __instance)
    {
        MelonLogger.Msg($"Loading modules for location: {Context.Loading}");
        var moduleLoader = new LoadingModuleLoader();
        moduleLoader.Kernel?.Bind<LoadingScene>().ToConstant(__instance).InSingletonScope();
        moduleLoader.BeginLoad();
    }
}