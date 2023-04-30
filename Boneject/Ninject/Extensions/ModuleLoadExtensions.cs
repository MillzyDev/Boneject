using System;
using System.Linq;
using Boneject.Ninject.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Boneject.Ninject;

public static class ModuleLoadExtensions
{
    
    /// <summary>
    /// Constructs and loads a module into the kernel with parameters.
    /// </summary>
    /// <param name="kernel">The kernel the module will be loaded into.</param>
    /// <param name="moduleType">The type of the module that will be loaded.</param>
    /// <param name="args">The arguments to construct it with.</param>
    /// <exception cref="BonejectException">Thrown if the module type does not inherit from <see cref="INinjectModule"/></exception>
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