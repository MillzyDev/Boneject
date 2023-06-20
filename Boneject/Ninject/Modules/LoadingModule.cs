using Ninject.Modules;
using SLZ.UI;

namespace Boneject.Ninject.Modules
{
    internal class LoadingModule : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;
        private readonly LoadingScene _loadingScene;

        public LoadingModule(int hostId, BonejectManager bonejectManager, LoadingScene loadingScene)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
            _loadingScene = loadingScene;
        }

        public override void Load()
        {
            Bind<LoadingScene>().ToConstant(_loadingScene).InSingletonScope();

            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
