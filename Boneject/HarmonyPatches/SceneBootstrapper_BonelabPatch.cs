using Boneject.Modules;
using HarmonyLib;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(SceneBootstrapper_Bonelab))]
    [HarmonyPatch(nameof(SceneBootstrapper_Bonelab.Start))]
    public static class SceneBootstrapper_BonelabPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(SceneBootstrapper_Bonelab __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            var baseModule = new SceneBootstrapperModule(__instance.gameObject.GetInstanceID(),
                bonejectManager, __instance);
            kernel.Load(baseModule);

            ContextUnloader.AddToObject(__instance.gameObject);
        }
    }
}
