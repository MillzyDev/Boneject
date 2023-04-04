namespace Boneject;

public interface IModuleLoaderFilter
{
    bool ShouldInstall(Type loaderType);
}