using System;
using Boneject.Filters;

namespace Boneject.Loading
{
    public class LoadSet
    {
        public readonly Type ModuleType;
        public readonly ILoadFilter LoadFilter;
        public readonly object[]? InitialArguments;

        public LoadSet(Type moduleType, ILoadFilter loadFilter, object[]? initialArguments = null)
        {
            ModuleType = moduleType;
            LoadFilter = loadFilter;
            InitialArguments = initialArguments;
        }
    }
}
