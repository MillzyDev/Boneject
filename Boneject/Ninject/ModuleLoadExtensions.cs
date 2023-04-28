using System;
using System.Linq;
using Boneject.Ninject.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Boneject.Ninject;

public static class ModuleLoadExtensions
{
    public static void Load(this IKernel kernel, Type moduleType, params object?[]? args)
    {
        Ensure.ArgumentNotNull(kernel, nameof(kernel));
        if (!typeof(INinjectModule).IsAssignableFrom(moduleType))
            throw new BonejectException("Cannot load type that does not inherit INinjectModule");

        INinjectModule module;
        if (args != null && moduleType.GetConstructors().Any(ctor => ctor.GetParameters().Length > 0))
            module = (INinjectModule)Activator.CreateInstance(moduleType, args);
        else
            module = (INinjectModule)Activator.CreateInstance(moduleType);
        
        kernel.Load(module);
    }
}