using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

namespace Boneject.Ninject.Filters
{
    internal class MultiTypedLoadFilter : ILoadFilter
    {
        private readonly IEnumerable<Type> _moduleTypes;

        public MultiTypedLoadFilter(IEnumerable<Type> moduleTypes)
        {
            Type[]? enumerable = moduleTypes as Type[] ?? moduleTypes.ToArray();

            foreach (Type? type in enumerable)
            {
                if (!typeof(INinjectModule).IsAssignableFrom(type))
                    throw new BonejectException($"Expected {type.Name} to derive of INinjectModule");
            }

            _moduleTypes = enumerable.ToArray();
        }

        public bool ShouldLoad(INinjectModule module)
        {
            return _moduleTypes.Any(type => type == module.GetType());
        }
    }
}
