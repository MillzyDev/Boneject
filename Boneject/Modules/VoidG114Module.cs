﻿using Ninject.Modules;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.VRMK;

namespace Boneject.Modules
{
    internal class VoidG114Module : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;
        private readonly GameControl_VoidG114 _gameControlVoidG114;
        private readonly RigManager _rigManager;
        private readonly BodyVitals _bodyVitals;

        public VoidG114Module(int hostId, BonejectManager bonejectManager, GameControl_VoidG114 gameControlVoidG114,
                              RigManager rigManager, BodyVitals bodyVitals)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
            _gameControlVoidG114 = gameControlVoidG114;
            _rigManager = rigManager;
            _bodyVitals = bodyVitals;
        }

        public override void Load()
        {
            Bind<GameControl_VoidG114>().ToConstant(_gameControlVoidG114).InSingletonScope();
            Bind<RigManager>().ToConstant(_rigManager).InSingletonScope();
            Bind<BodyVitals>().ToConstant(_bodyVitals).InSingletonScope();

            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
