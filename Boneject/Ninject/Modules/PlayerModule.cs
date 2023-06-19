using System.Linq;
using Ninject.Modules;
using SLZ.Rig;

namespace Boneject.Ninject.Modules
{
    internal class PlayerModule : NinjectModule
    {
        private readonly BonejectManager _bonejectManager;

        private readonly RigManager _rigManager;

        public PlayerModule(BonejectManager bonejectManager, RigManager rigManager)
        {
            _bonejectManager = bonejectManager;

            _rigManager = rigManager;
        }

        public override void Load()
        {
            Rebind<RigManager>().ToConstant(_rigManager).InSingletonScope();
            _bonejectManager.LoadForContext(this);
        }
    }
}
