using Ninject.Modules;
using SLZ.UI;

namespace Boneject.Ninject.Modules;

public class LoadingModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;
    private readonly LoadingScene _loadingScene;
    
    public LoadingModule(BonejectManager bonejectManager, LoadingScene loadingScene)
    {
        _bonejectManager = bonejectManager;
        _loadingScene = loadingScene;
    }
    
    public override void Load()
    {
        Bind<LoadingScene>().ToConstant(_loadingScene).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}