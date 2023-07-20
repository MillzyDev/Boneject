using System.Collections.Generic;
using System.Linq;

namespace Boneject.Filters
{
    public class MultiSceneLoadFilter : ISceneLoadFilter
    {
        private readonly IEnumerable<string> _sceneNames;

        public MultiSceneLoadFilter(IEnumerable<string> sceneNames)
        {
            _sceneNames = sceneNames;
        }

        public bool ShouldLoad(string sceneName)
        {
            return _sceneNames.Any(otherSceneName => sceneName == otherSceneName);
        }
    }
}
