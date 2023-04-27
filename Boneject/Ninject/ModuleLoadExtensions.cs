using System;
using System.Reflection;
using Boneject.Ninject.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Boneject.Ninject;

public static class ModuleLoadExtensions
{
    public static void Load(this IKernel kernel, Type moduleType, params object?[]? args)
    {
        Ensure.ArgumentNotNull(kernel, nameof(kernel));
        if (!moduleType.IsSubclassOf(typeof(INinjectModule)))
            throw new BonejectException("Cannot load type that does not inherit INinjectModule");
        
        var module = (INinjectModule)Activator.CreateInstance(moduleType, BindingFlags.Public | BindingFlags.NonPublic, args);
        kernel.Load(module);
    }
}