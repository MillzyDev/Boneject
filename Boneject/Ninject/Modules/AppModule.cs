using Ninject.Modules;

namespace Boneject.Ninject.Modules;

public class AppModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;
    
    public AppModule()
    {
        _bonejectManager = Mod.BonejectManager;
    }
    
    public override void Load()
    {
        _bonejectManager.LoadForContext(this);
    }

}