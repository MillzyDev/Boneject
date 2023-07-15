using Ninject.Modules;

namespace Boneject.Modules
{
    internal class AppModule : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;

        public AppModule(int hostId, BonejectManager bonejectManager)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
        }

        public override void Load()
        {
            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
