using MelonLoader;
using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.Modules;

public class BonelabModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;

    private readonly BonelabGameControl _bonelabGameControl;
    private readonly RigManager _rigManager;
    private readonly SaveFeatures _saveFeatures;
    private readonly InventorySaveFilter _inventorySaveFilter;
    
    public BonelabModule(BonejectManager bonejectManager, BonelabGameControl bonelabGameControl, RigManager rigManager,
        SaveFeatures saveFeatures, InventorySaveFilter inventorySaveFilter)
    {
        _bonejectManager = bonejectManager;
        
        _bonelabGameControl = bonelabGameControl;
        _rigManager = rigManager;
        _saveFeatures = saveFeatures;
        _inventorySaveFilter = inventorySaveFilter;
    }

    public override void Load()
    {
        MelonLogger.Msg("Bonelab Module");
        Bind<BonelabGameControl>().ToConstant(_bonelabGameControl).InSingletonScope();
        Bind<RigManager>().ToConstant(_rigManager).InSingletonScope();
        Bind<SaveFeatures>().ToConstant(_saveFeatures).InSingletonScope();
        Bind<InventorySaveFilter>().ToConstant(_inventorySaveFilter).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}