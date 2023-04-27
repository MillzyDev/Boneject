using System;
using System.Collections.Generic;
using System.Linq;
using Ninject.Modules;

namespace Boneject.Ninject.Filters;

internal class MultiTypedLoadFilter : ILoadFilter
{
    private readonly IEnumerable<Type> _moduleTypes;

    public MultiTypedLoadFilter(IEnumerable<Type> moduleTypes)
    {
        var enumerable = moduleTypes as Type[] ?? moduleTypes.ToArray();
        foreach (var type in enumerable)
        {
            if (!type.IsSubclassOf(typeof(INinjectModule)))
                throw new BonejectException($"Expected {type.Name} to be a subclass of INinjectModule");
        }

        _moduleTypes = enumerable.ToArray();
    }
    
    public bool ShouldLoad(INinjectModule module)
    {
        return _moduleTypes.Any(type => type == module.GetType());
    }
}