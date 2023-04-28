using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Interaction;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.Modules;

public class BonelabModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;

    private readonly BonelabGameControl _bonelabGameControl;
    private readonly SaveFeatures _saveFeatures;
    private readonly InventorySaveFilter _inventorySaveFilter;
    
    public BonelabModule(BonejectManager bonejectManager, BonelabGameControl bonelabGameControl,
        SaveFeatures saveFeatures, InventorySaveFilter inventorySaveFilter)
    {
        _bonejectManager = bonejectManager;
        
        _bonelabGameControl = bonelabGameControl;
        _saveFeatures = saveFeatures;
        _inventorySaveFilter = inventorySaveFilter;
    }

    public override void Load()
    {
        Bind<BonelabGameControl>().ToConstant(_bonelabGameControl).InSingletonScope();
        Bind<SaveFeatures>().ToConstant(_saveFeatures).InSingletonScope();
        Bind<InventorySaveFilter>().ToConstant(_inventorySaveFilter).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}