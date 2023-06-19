using System;
using System.Linq;
using Ninject.Activation;
using Ninject.Syntax;

namespace Boneject.Ninject.Extensions
{
    // ReSharper disable once UnusedType.Global
    public static class BindingExtensions
    {
        /// <summary>
        /// Binds all interfaces of <typeparamref name="T"/> to <typeparamref name="T"/>.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<T> BindInterfacesTo<T>(this IBindingRoot self)
        {
            return self.Bind(typeof(T).GetInterfaces()).To<T>();
        }

        /// <summary>
        /// Binds all interfaces of <paramref name="type"/> to <paramref name="type"/>
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<object> BindInterfacesTo(
            this IBindingRoot self, Type type)
        {
            return self.Bind(type.GetInterfaces()).To(type);
        }

        /// <summary>
        /// Binds all interfaces of <typeparamref name="T"/> and <typeparamref name="T"/> to <typeparamref name="T"/>
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<T> BindInterfacesAndSelfTo<T>(this IBindingRoot self)
        {
            return self.Bind(typeof(T).GetInterfaces().Concat(new[] { typeof(T) }).ToArray()).To<T>();
        }

        /// <summary>
        /// Binds all interfaces of <paramref name="type"/> and <paramref name="type"/> to <paramref name="type"/>.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<object> BindInterfacesAndSelfTo(
            this IBindingRoot self, Type type)
        {
            return self.Bind(type.GetInterfaces().Concat(new[] { type }).ToArray()).To(type);
        }

        /// <summary>
        /// Binds an instance of <typeparamref name="T"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="constant">The instance of T to bind.</param>
        // ReSharper disable once InvalidXmlDocComment
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<T> BindConstant<T>(this IBindingRoot self, T constant)
        {
            return self.Bind<T>().ToConstant(constant);
        }

        /// <summary>
        /// Binds an instance of <paramref name="constant"/> to the type of <paramref name="constant"/>.
        /// </summary>
        /// <param name="constant">The instance to bind.</param>
        // ReSharper disable once InvalidXmlDocComment
        // ReSharper disable once UnusedMember.Global
        public static IBindingWhenInNamedWithOrOnSyntax<object> BindConstant(
            this IBindingRoot self, object constant)
        {
            return self.Bind(constant.GetType()).ToConstant(constant);
        }

        /// <summary>
        /// Binds every instance in <paramref name="constants"/> to the type of it's instance in the Singleton Scope.
        /// </summary>
        /// <param name="constants">The constants to bind.</param>
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InvalidXmlDocComment
        public static void BindConstants(this IBindingRoot self, params object[] constants)
        {
            Array.ForEach(constants,
                constant => self.Bind(constant.GetType()).ToConstant(constant).InSingletonScope());
        }

        /// <summary>
        /// Binds every instance of <paramref name="constants"/> to the type of it's instance in the specified scope.
        /// </summary>
        /// <param name="scope">The scope to bind into.</param>
        /// <param name="constants">The constants to bind.</param>
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InvalidXmlDocComment
        public static void BindConstantsInScope(this IBindingRoot self, Func<IContext, object> scope,
                                                params object[] constants)
        {
            Array.ForEach(constants,
                constant => self.Bind(constant.GetType()).ToConstant(constant).InScope(scope));
        }
    }
}
