using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(GameControl_MenuVoidG114))]
[HarmonyPatch(nameof(GameControl_MenuVoidG114.Start))]
public static class GameControl_MenuVoidG114Patch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Global
    public static void Postfix(GameControl_MenuVoidG114 __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var controlPlayer = __instance.controlPlayer;
        var bodyVitals = __instance.ctrl_bodyVitals;
        var laserCursor = __instance.mainMenuUIController;
        var fadeVolume = __instance.fadeVolume;

        var baseModule = new VoidG114MenuModule(bonejectManager, __instance, controlPlayer, bodyVitals, laserCursor,
            fadeVolume);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("VoidG114Menu context loaded.");
    }
}