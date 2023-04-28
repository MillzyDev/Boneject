using Ninject;
using Ninject.Modules;

namespace Boneject.Ninject;

public class BonejectKernel : StandardKernel
{
    public BonejectKernel(params INinjectModule[] modules) : base(modules)
    {
    }

    public BonejectKernel(INinjectSettings settings, params INinjectModule[] modules) : base(settings, modules)
    {
    }
}