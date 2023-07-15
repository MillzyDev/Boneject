using Ninject;
using Ninject.Modules;

namespace Boneject
{
    public class BonejectKernel : StandardKernel
    {
        // I made this in case I need to add some functionality to the kernel later on without making any breaking changes

        public BonejectKernel(INinjectSettings settings, params INinjectModule[] modules) : base(settings,
            modules)
        {
        }
    }
}
