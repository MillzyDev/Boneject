using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.SaveData;

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
        if (__instance is not BonelabInternalGameControl bonelabInternalGameControl) return;

        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var rigManager = bonelabInternalGameControl.PlayerRigManager;
        var saveFeatures = bonelabInternalGameControl.SaveFeatures;
        var inventorySaveFilter = bonelabInternalGameControl.InventorySaveFilter;

        var campaignModule = new CampaignModule(bonejectManager, bonelabInternalGameControl, rigManager, saveFeatures, inventorySaveFilter);
        kernel.Load(campaignModule);
        
        MelonLogger.Msg("Campaign context loaded.");
    }
}