using System.Linq;
using Ninject.Modules;
using SLZ.Rig;

namespace Boneject.Ninject.Modules
{
    internal class PlayerModule : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;
        private readonly RigManager _rigManager;

        public PlayerModule(int hostId, BonejectManager bonejectManager, RigManager rigManager)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
            _rigManager = rigManager;
        }

        public override void Load()
        {
            Rebind<RigManager>().ToConstant(_rigManager).InSingletonScope();
            
            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
