using Ninject.Modules;

namespace Boneject.Ninject.Modules;

internal class AppModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;
    
    public AppModule(BonejectManager bonejectManager)
    {
        _bonejectManager = bonejectManager;
    }
    
    public override void Load()
    {
        // funny "scene name" is used so that it's never unloaded/cleared
        _bonejectManager.LoadForContext(this, "__APP_CONTEXT_PRESERVE__");
    }

}