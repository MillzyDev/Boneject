using Ninject.Modules;
using UnhollowerBaseLib.Attributes;

namespace Boneject.Ninject.Modules;

public class AppModule : NinjectModule
{
    private readonly BonejectManager _bonejectManager;
    
    public AppModule(BonejectManager bonejectManager)
    {
        _bonejectManager = bonejectManager;
    }
    
    public override void Load()
    {
        _bonejectManager.LoadForContext(this);
    }

}