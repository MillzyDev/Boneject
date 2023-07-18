using Boneject.Filters;

namespace Boneject.Loading
{
    public struct SceneLoadSet
    {
        public readonly string SceneName;
        public readonly ISceneLoadFilter LoadFilter;
        public readonly object[]? InitialArguments;

        public SceneLoadSet(string sceneName, ISceneLoadFilter loadFilter, object[]? initialArguments = null)
        {
            SceneName = sceneName;
            LoadFilter = loadFilter;
            InitialArguments = initialArguments;
        }
    }
}
