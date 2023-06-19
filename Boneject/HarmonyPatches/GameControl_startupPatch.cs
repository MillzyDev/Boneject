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
    [HarmonyPatch(typeof(GameControl_startup))]
    [HarmonyPatch(nameof(GameControl_startup.Start))]
    public static class GameControl_startupPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_startup __instance)
        {
            BonejectManager? bonejectManager = Mod.BonejectManager;
            BonejectKernel? kernel = bonejectManager.Kernel;

            Control_Player? controlPlayer = __instance.controlPlayer;
            BodyVitals? bodyVitals = __instance.ctrl_bodyVitals;
            LaserCursor? laserCursor = __instance.mainMenuUIController;
            FadeAndDisableVolume? fadeVolume = __instance.fadeVolume;

            var baseModule = new StartupModule(bonejectManager, __instance, controlPlayer, bodyVitals,
                laserCursor,
                fadeVolume);
            kernel.Load(baseModule);

            MelonLogger.Msg("Startup context loaded.");
        }
    }
}
