using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.ContextLoaders
{
    // this will be removed once ML 6.1 is used for BONELAB mods, CodeGen will be used to make an adapter.
    internal static class CampaignContextLoader
    {
        public static void Load(int hostId, BonelabInternalGameControl bonelabInternalGameControl)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            RigManager? rigManager = bonelabInternalGameControl.PlayerRigManager;
            SaveFeatures saveFeatures = bonelabInternalGameControl.SaveFeatures;
            InventorySaveFilter? inventorySaveFilter = bonelabInternalGameControl.InventorySaveFilter;

            var baseModule = new CampaignModule(hostId, bonejectManager, bonelabInternalGameControl, rigManager,
                saveFeatures,
                inventorySaveFilter);
            kernel.Load(baseModule);

            MelonLogger.Msg("Campaign context loaded.");
        }
    }
}
