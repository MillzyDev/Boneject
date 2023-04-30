using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.Modules;

internal class CampaignModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;

    private readonly BonelabInternalGameControl _bonelabInternalGameControl;
    private readonly RigManager _rigManager;
    private readonly SaveFeatures _saveFeatures;
    private readonly InventorySaveFilter _inventorySaveFilter;

    public CampaignModule(BonejectManager bonejectManager, BonelabInternalGameControl bonelabInternalGameControl, 
        RigManager rigManager, SaveFeatures saveFeatures, InventorySaveFilter inventorySaveFilter)
    {
        _bonejectManager = bonejectManager;
        _bonelabInternalGameControl = bonelabInternalGameControl;
        _rigManager = rigManager;
        _saveFeatures = saveFeatures;
        _inventorySaveFilter = inventorySaveFilter;
    }

    
    public override void Load()
    {
        Bind<BonelabInternalGameControl>().ToConstant(_bonelabInternalGameControl!).InSingletonScope();
        Bind<RigManager>().ToConstant(_rigManager!).InSingletonScope();
        Bind<SaveFeatures>().ToConstant(_saveFeatures).InSingletonScope();
        Bind<InventorySaveFilter>().ToConstant(_inventorySaveFilter!).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}