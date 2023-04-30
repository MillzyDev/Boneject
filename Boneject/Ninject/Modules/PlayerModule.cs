﻿using System.Linq;
using Ninject.Modules;
using SLZ.Rig;

namespace Boneject.Ninject.Modules;

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
        // This is loaded later than BonelabGameControl. We want RigManager to still be bound in those contexts without
        // conflicting, so we check if a binding for it already exists.
        if (Kernel!.GetBindings(typeof(RigManager)).Any())
            Bind<RigManager>().ToConstant(_rigManager).InSingletonScope();
        
        _bonejectManager.LoadForContext(this);
    }
}