using System;

namespace Boneject;


// TODO: Make filtering better with this
public interface IModuleLoaderFilter
{
    bool ShouldInstall(Type loaderType);
}