using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Activation;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Syntax;
using UnityEngine;

namespace Boneject;

// ReSharper disable once UnusedType.Global
public static class BindingExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnNewGameObject<T>(this KernelBase self) 
        where T : Component
    {
        var bindingSyntax = BindComponentOnNewGameObject<T>(self, out _);
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnNewGameObject<T>(this NinjectModule self)
        where T : Component
    {
        var bindingSyntax = BindComponentOnNewGameObject<T>((self.Kernel as KernelBase)!, out _);
        return bindingSyntax;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnNewGameObject<T>(this KernelBase self, 
        out GameObject gameObject) where T : Component
    {
        gameObject = new GameObject(typeof(T).FullName);
        var bindingSyntax = BindComponentOnExistingGameObject<T>(self, gameObject);
        return bindingSyntax;
    }
    
    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnNewGameObject<T>(this NinjectModule self, 
        out GameObject gameObject) where T : Component
    {
        gameObject = new GameObject(typeof(T).FullName);
        var bindingSyntax = BindComponentOnExistingGameObject<T>((self.Kernel as KernelBase)!, gameObject);
        return bindingSyntax;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnExistingGameObject<T>(this KernelBase self,
        GameObject gameObject) where T : Component
    {
        gameObject.SetActive(false);
        
        var instance = gameObject.AddComponent<T>();
        self.Inject(instance);
        
        var bindingSyntax = self.Bind<T>().ToConstant(instance);
        
        gameObject.SetActive(true);
        
        return bindingSyntax;
    }
    
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnExistingGameObject<T>(this NinjectModule self,
        GameObject gameObject) where T : Component
    {
        return BindComponentOnExistingGameObject<T>((self.Kernel as KernelBase)!, gameObject);
    }

    /**
     * ZENJECT PORTS
     */

    public static IBindingWhenInNamedWithOrOnSyntax<T> BindInterfacesAndSelfTo<T>(this IBindingRoot self)
    {
        var interfaces = typeof(T).GetInterfaces();
        var types = new Type[interfaces.Length + 1];
        types[0] = typeof(T);
        interfaces.CopyTo(types, 1);
        return self.Bind(types).To<T>();
    }

    public static IBindingWhenInNamedWithOrOnSyntax<T> BindInterfacesTo<T>(this IBindingRoot self) =>
        self.Bind(typeof(T).GetInterfaces()).To<T>();

    public static IBindingWhenInNamedWithOrOnSyntax<T> BindConstant<T>(this IBindingRoot self, T instance) =>
        self.Bind<T>().ToConstant(instance);

    public static void BindConstants(this IBindingRoot self, params object[] instances)
    {
        foreach (var instance in instances)
        {
            self.Bind(instance.GetType()).ToConstant(instance);
        }
    }
    
    /**
     * BACK TO OUR OWN THINGS
     */
    
    public static void BindConstantsInScope(this IBindingRoot self, Func<IContext, object> scopeCallback, params object[] instances)
    {
        foreach (var instance in instances)
        {
            self.Bind(instance.GetType()).ToConstant(instance).InScope(scopeCallback);
        }
    }
    
    public static void BindConstantsInSingletonScope(this IBindingRoot self, params object[] instances)
    {
        foreach (var instance in instances)
        {
            self.Bind(instance.GetType()).ToConstant(instance).InSingletonScope();
        }
    }
    
    public static void BindConstantsInTransientScope(this IBindingRoot self, params object[] instances)
    {
        foreach (var instance in instances)
        {
            self.Bind(instance.GetType()).ToConstant(instance).InTransientScope();
        }
    }
    
    public static void BindConstantsInThreadScope(this IBindingRoot self, params object[] instances)
    {
        foreach (var instance in instances)
        {
            self.Bind(instance.GetType()).ToConstant(instance).InThreadScope();
        }
    }
}