using System;
using Boneject.Ninject.Filters;

namespace Boneject.Ninject;

internal struct LoadSet
{
    public readonly Type ModuleType;
    public readonly ILoadFilter LoadFilter;
    public readonly object[]? InitialParameters;

    public LoadSet(Type moduleType, ILoadFilter loadFilter, object[]? initialParameters = null)
    {
        ModuleType = moduleType;
        LoadFilter = loadFilter;
        InitialParameters = initialParameters;
    }
}