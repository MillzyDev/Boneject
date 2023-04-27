using Ninject.Modules;

namespace Boneject.Ninject.Filters;

internal interface ILoadFilter
{
    public bool ShouldLoad(INinjectModule baseModule);
}