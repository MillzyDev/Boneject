using Boneject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.HarmonyPatches
{
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

            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            RigManager? rigManager = bonelabInternalGameControl.PlayerRigManager;
            SaveFeatures saveFeatures = bonelabInternalGameControl.SaveFeatures;
            InventorySaveFilter? inventorySaveFilter = bonelabInternalGameControl.InventorySaveFilter;

            var campaignModule = new CampaignModule(__instance.gameObject.GetInstanceID(), bonejectManager, 
                bonelabInternalGameControl, rigManager, saveFeatures, inventorySaveFilter);
            kernel.Load(campaignModule);

            ContextUnloader.AddToObject(__instance.gameObject);

            MelonLogger.Msg("Campaign context loaded.");
        }
    }
}
