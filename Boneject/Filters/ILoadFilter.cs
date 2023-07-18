using Ninject.Modules;

namespace Boneject.Filters
{
    public interface ILoadFilter
    {
        public bool ShouldLoad(INinjectModule baseModule);
    }
}
