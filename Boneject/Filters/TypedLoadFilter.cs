using System;
using Ninject.Modules;

namespace Boneject.Filters
{
    public class TypedLoadFilter : ILoadFilter
    {
        private readonly Type _moduleType;

        public TypedLoadFilter(Type moduleType)
        {
            if (!typeof(INinjectModule).IsAssignableFrom(moduleType))
                throw new ArgumentException($"Expected {moduleType.Name} to derive from INinjectModule");
            _moduleType = moduleType;
        }

        public bool ShouldLoad(INinjectModule baseModule)
        {
            return _moduleType == baseModule.GetType();
        }
    }
}
