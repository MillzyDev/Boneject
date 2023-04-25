using System;
using System.Collections.Generic;
using System.Linq;
using MelonLoader.Assertions;
using Ninject.Modules;

namespace Boneject.Filters;

public class MultiTypedInstallFilter : IInstallFilter
{
    private readonly IEnumerable<Type> _moduleTypes;

    public MultiTypedInstallFilter(IEnumerable<Type> moduleTypes)
    {
        var enumerable = moduleTypes.ToList();
        foreach (var type in enumerable)
            Assert.DerivesFrom<ModuleLoader>(type);
        _moduleTypes = enumerable;
    }

    public bool ShouldInstall(Type moduleLoaderType)
    {
        return _moduleTypes.Any(type => type == moduleLoaderType);
    }
}