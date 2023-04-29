using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.Modules;

public class CampaignModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;

    private readonly BonelabInternalGameControl? _bonelabInternalGameControl;
    private readonly RigManager? _rigManager;
    private readonly SaveFeatures _saveFeatures;
    private readonly InventorySaveFilter? _inventorySaveFilter;

    public CampaignModule(BonejectManager bonejectManager)
    {
        _bonejectManager = bonejectManager;
    }

    public CampaignModule(BonejectManager bonejectManager, BonelabInternalGameControl bonelabInternalGameControl, 
        RigManager rigManager, SaveFeatures saveFeatures, InventorySaveFilter inventorySaveFilter) : 
        this(bonejectManager)
    {
        _bonelabInternalGameControl = bonelabInternalGameControl;
        _rigManager = rigManager;
        _saveFeatures = saveFeatures;
        _inventorySaveFilter = inventorySaveFilter;
    }

    
    public override void Load()
    {
        if (_bonelabInternalGameControl is not null)
            Bind<BonelabInternalGameControl>().ToConstant(_bonelabInternalGameControl!).InSingletonScope();
        if (_rigManager is not null)
            Bind<RigManager>().ToConstant(_rigManager!).InSingletonScope();
        if (_saveFeatures == 0)
            Bind<SaveFeatures>().ToConstant(_saveFeatures).InSingletonScope();
        if (_inventorySaveFilter is not null)
            Bind<InventorySaveFilter>().ToConstant(_inventorySaveFilter!).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}