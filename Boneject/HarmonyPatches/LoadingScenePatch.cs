using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.UI;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(LoadingScene))]
[HarmonyPatch(nameof(LoadingScene.Start))]
public static class LoadingScenePatch
{
    [HarmonyPostfix]
    // ReSharper disable once UnusedMember.Local
    // ReSharper disable once InconsistentNaming
    private static void Postfix(LoadingScene __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var baseModule = new LoadingModule(bonejectManager, __instance);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("Loading context loaded.");
    }
}