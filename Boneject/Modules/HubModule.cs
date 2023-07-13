using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.Ninject.Modules
{
    internal class HubModule : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;
        private readonly GameControl_Hub _gameControlHub;
        private readonly RigManager _rigManager;
        private readonly Control_Player _controlPlayer;
        private readonly GauntletElevator _gauntletElevator;
        private readonly InventorySaveFilter _inventorySaveFilter;

        public HubModule(int hostId, BonejectManager bonejectManager, GameControl_Hub gameControlHub,
                         RigManager rigManager,
                         Control_Player controlPlayer, GauntletElevator gauntletElevator,
                         InventorySaveFilter inventorySaveFilter)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
            _gameControlHub = gameControlHub;
            _rigManager = rigManager;
            _controlPlayer = controlPlayer;
            _gauntletElevator = gauntletElevator;
            _inventorySaveFilter = inventorySaveFilter;
        }

        public override void Load()
        {
            Bind<GameControl_Hub>().ToConstant(_gameControlHub).InSingletonScope();
            Bind<RigManager>().ToConstant(_rigManager).InSingletonScope();
            Bind<Control_Player>().ToConstant(_controlPlayer).InSingletonScope();
            Bind<GauntletElevator>().ToConstant(_gauntletElevator).InSingletonScope();
            Bind<InventorySaveFilter>().ToConstant(_inventorySaveFilter).InSingletonScope();

            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
