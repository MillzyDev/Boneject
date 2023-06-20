using Boneject.Ninject;
using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.VRMK;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(GameControl_VoidG114))]
    [HarmonyPatch(nameof(GameControl_VoidG114.Start))]
    public static class GameControl_VoidG114Patch
    {
        [HarmonyPostfix]
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        private static void Postfix(GameControl_VoidG114 __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            RigManager? rigManager = __instance.rm;
            BodyVitals? bodyVitals = __instance.ctrl_bodyVitals;

            var baseModule = new VoidG114Module(__instance.gameObject.GetInstanceID(), bonejectManager, __instance, rigManager, bodyVitals);
            kernel.Load(baseModule);

            ContextUnloader.AddToObject(__instance.gameObject);

            MelonLogger.Msg("VoidG114 context loaded.");
        }
    }
}
