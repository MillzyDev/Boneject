using Boneject.Ninject;
using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Rig;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(RigManager))]
    [HarmonyPatch(nameof(RigManager.Start))]
    public static class RigManagerPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(RigManager __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            var baseModule = new PlayerModule(__instance.gameObject.GetInstanceID(), bonejectManager, __instance);
            kernel.Load(baseModule);

            ContextUnloader.AddToObject(__instance.gameObject);
            

            MelonLogger.Msg("Player context loaded.");
        }
    }
}
