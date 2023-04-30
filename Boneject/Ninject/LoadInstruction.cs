using System;

namespace Boneject.Ninject;

internal struct LoadInstruction
{
    public readonly Type baseModule;
    public readonly Action<BonejectKernel> onLoad;

    public LoadInstruction(Type baseModule, Action<BonejectKernel> onLoad)
    {
        this.baseModule = baseModule;
        this.onLoad = onLoad;
    }
}