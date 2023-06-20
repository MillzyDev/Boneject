using Boneject.Ninject;
using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.UI;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(LoadingScene))]
    [HarmonyPatch(nameof(LoadingScene.Start))]
    public static class LoadingScenePatch
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        private static void Postfix(LoadingScene __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            var baseModule = new LoadingModule(__instance.gameObject.GetInstanceID(), bonejectManager, __instance);
            kernel.Load(baseModule);

            ContextUnloader.AddToObject(__instance.gameObject);

            MelonLogger.Msg("Loading context loaded.");
        }
    }
}
