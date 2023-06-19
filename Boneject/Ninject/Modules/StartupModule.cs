using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.UI;
using SLZ.VRMK;

namespace Boneject.Ninject.Modules
{
    internal class StartupModule : NinjectModule
    {
        private readonly BonejectManager _bonejectManager;
        private readonly GameControl_startup _gameControlStartup;
        private readonly Control_Player _controlPlayer;
        private readonly BodyVitals _bodyVitals;
        private readonly LaserCursor _laserCursor;
        private readonly FadeAndDisableVolume _fadeVolume;

        public StartupModule(BonejectManager bonejectManager, GameControl_startup gameControlStartup,
                             Control_Player controlPlayer, BodyVitals bodyVitals, LaserCursor laserCursor,
                             FadeAndDisableVolume fadeVolume)
        {
            _bonejectManager = bonejectManager;
            _gameControlStartup = gameControlStartup;
            _controlPlayer = controlPlayer;
            _bodyVitals = bodyVitals;
            _laserCursor = laserCursor;
            _fadeVolume = fadeVolume;
        }

        public override void Load()
        {
            Bind<GameControl_startup>().ToConstant(_gameControlStartup).InSingletonScope();
            Bind<Control_Player>().ToConstant(_controlPlayer).InSingletonScope();
            Bind<BodyVitals>().ToConstant(_bodyVitals).InSingletonScope();
            Bind<LaserCursor>().ToConstant(_laserCursor).InSingletonScope();
            Bind<FadeAndDisableVolume>().ToConstant(_fadeVolume).InSingletonScope();

            _bonejectManager.LoadForContext(this);
        }
    }
}
