namespace Boneject.Filters
{
    public class SceneLoadFilter : ISceneLoadFilter
    {
        private readonly string _sceneName;

        public SceneLoadFilter(string sceneName)
        {
            _sceneName = sceneName;
        }
        
        public bool ShouldLoad(string sceneName)
        {
            return sceneName == _sceneName;
        }
    }
}
