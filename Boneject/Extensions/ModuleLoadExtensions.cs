using System;
using System.Linq;
using Boneject.Infrastructure;
using Ninject;
using Ninject.Modules;

namespace Boneject.Extensions
{
    public static class ModuleLoadExtensions
    {
        /// <summary>
        /// Constructs and loads a module into the kernel with parameters.
        /// </summary>
        /// <param name="moduleType">The type of the module that will be loaded.</param>
        /// <param name="args">The arguments to construct it with.</param>
        /// <exception cref="BonejectException">Thrown if the module type does not inherit from <see cref="INinjectModule"/></exception>
        // ReSharper disable once InvalidXmlDocComment
        public static INinjectModule Load(this IKernel kernel, Type moduleType, params object?[]? args)
        {
            Ensure.ArgumentNotNull(kernel, nameof(kernel));
            if (!typeof(INinjectModule).IsAssignableFrom(moduleType))
                throw new BonejectException("Cannot load type that does not inherit INinjectModule");

            INinjectModule module;
            // TODO: replace with expression
            if (args != null && moduleType.GetConstructors().Any(ctor => ctor.GetParameters().Length > 0))
                module = (INinjectModule)Activator.CreateInstance(moduleType, args);
            else
                module = (INinjectModule)Activator.CreateInstance(moduleType);
            
            kernel.Load(module);

            return module;
        }
    }
}
