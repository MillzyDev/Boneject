using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches;

[HarmonyPatch(typeof(BonelabGameControl))]
[HarmonyPatch(nameof(BonelabGameControl.Start))]
public static class BonelabGameControlPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(BonelabGameControl __instance)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var rigManager = __instance.PlayerRigManager;
        var saveFeatures = __instance.SaveFeatures;
        var inventorySaveFilter = __instance.InventorySaveFilter;

        var baseModule = new BonelabModule(bonejectManager, __instance, rigManager, saveFeatures, inventorySaveFilter);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("Bonelab context loaded.");

        if (__instance is not BonelabInternalGameControl) return; // just in case
        
        var campaignModule = new CampaignModule(bonejectManager);
        kernel.Load(campaignModule);
        
        MelonLogger.Msg("Campaign context loaded.");

    }
}