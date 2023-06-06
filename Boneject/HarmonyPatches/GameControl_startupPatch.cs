using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_startup))]
[HarmonyPatch(nameof(GameControl_startup.Start))]
public static class GameControl_startupPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_startup __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var controlPlayer = __instance.controlPlayer;
        var bodyVitals = __instance.ctrl_bodyVitals;
        var laserCursor = __instance.mainMenuUIController;
        var fadeVolume = __instance.fadeVolume;

        var baseModule = new StartupModule(bonejectManager, __instance, controlPlayer, bodyVitals, laserCursor, 
            fadeVolume);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("Startup context loaded.");
    }
}