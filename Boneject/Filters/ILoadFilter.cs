using Ninject.Modules;

namespace Boneject.Filters
{
    // The part of SiraUtil that this is based on had other filters for other features, this is just here in case I port those features/
    internal interface ILoadFilter
    {
        public bool ShouldLoad(INinjectModule baseModule);
    }
}
