using System;

namespace Boneject;


// TODO: Make filtering better with this
public interface IInstallFilter
{
    bool ShouldInstall(Type loaderType);
}