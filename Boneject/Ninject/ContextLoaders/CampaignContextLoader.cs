using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;

namespace Boneject.Ninject.ContextLoaders;

internal static class CampaignContextLoader
{
    public static void Load(BonelabInternalGameControl bonelabInternalGameControl)
    {
        var bonejectManager = Mod.BonejectManager;
        var kernel = bonejectManager.Kernel;

        var rigManager = bonelabInternalGameControl.PlayerRigManager;
        var saveFeatures = bonelabInternalGameControl.SaveFeatures;
        var inventorySaveFilter = bonelabInternalGameControl.InventorySaveFilter;

        var baseModule = new CampaignModule(bonejectManager, bonelabInternalGameControl, rigManager, saveFeatures, 
            inventorySaveFilter);
        kernel.Load(baseModule);
        
        MelonLogger.Msg("Campaign context loaded.");
    }
}