namespace Boneject.Filters
{
    public interface ISceneLoadFilter
    {
        public bool ShouldLoad(string sceneName);
    }
}
