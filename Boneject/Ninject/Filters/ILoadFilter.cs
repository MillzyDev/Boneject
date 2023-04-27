using Ninject.Modules;

namespace Boneject.Ninject.Filters;

public interface ILoadFilter
{
    public bool ShouldLoad(NinjectModule module);
}