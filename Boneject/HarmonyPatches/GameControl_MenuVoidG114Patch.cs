using Boneject.Ninject;
using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.UI;
using SLZ.VRMK;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(GameControl_MenuVoidG114))]
    [HarmonyPatch(nameof(GameControl_MenuVoidG114.Start))]
    public static class GameControl_MenuVoidG114Patch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Global
        public static void Postfix(GameControl_MenuVoidG114 __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            Control_Player? controlPlayer = __instance.controlPlayer;
            BodyVitals? bodyVitals = __instance.ctrl_bodyVitals;
            LaserCursor? laserCursor = __instance.mainMenuUIController;
            FadeAndDisableVolume? fadeVolume = __instance.fadeVolume;

            var baseModule = new VoidG114MenuModule(__instance.gameObject.GetInstanceID(), bonejectManager, 
                __instance, controlPlayer, bodyVitals, laserCursor, fadeVolume);
            kernel.Load(baseModule);

            MelonLogger.Msg("VoidG114Menu context loaded.");
        }
    }
}
