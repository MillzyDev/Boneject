using System;
using Boneject.Filters;

namespace Boneject.Loading
{
    public struct SceneLoadSet
    {
        public readonly Type ModuleType;
        public readonly ISceneLoadFilter LoadFilter;
        public readonly object[]? InitialArguments;

        public SceneLoadSet(Type moduleType, ISceneLoadFilter loadFilter, object[]? initialArguments = null)
        {
            ModuleType = moduleType;
            LoadFilter = loadFilter;
            InitialArguments = initialArguments;
        }
    }
}
