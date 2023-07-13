using Ninject.Modules;
using SLZ.Bonelab;

namespace Boneject.Ninject.Modules
{
    internal class SceneBootstrapperModule : NinjectModule
    {
        private readonly int _hostId;
        private readonly BonejectManager _bonejectManager;
        private readonly SceneBootstrapper_Bonelab _sceneBootstrapperBonelab;

        public SceneBootstrapperModule(int hostId, BonejectManager bonejectManager,
                                       SceneBootstrapper_Bonelab sceneBootstrapperBonelab)
        {
            _hostId = hostId;
            _bonejectManager = bonejectManager;
            _sceneBootstrapperBonelab = sceneBootstrapperBonelab;
        }
        
        public override void Load()
        {
            Bind<SceneBootstrapper_Bonelab>().ToConstant(_sceneBootstrapperBonelab).InSingletonScope();
            
            _bonejectManager.LoadForContext(this, _hostId);
        }
    }
}
