using System;
using Ninject.Modules;

namespace Boneject;


// TODO: Make filtering better with this
public interface IInstallFilter
{
    bool ShouldInstall(Type moduleLoaderType);
}