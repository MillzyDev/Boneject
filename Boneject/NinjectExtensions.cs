using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using UnityEngine;

namespace Boneject;

// ReSharper disable once UnusedType.Global
public static class NinjectExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> AsComponentOnExistingGameObject<T>(
        this IBindingToSyntax<T> self, GameObject gameObject) where T : Component
    {
        var instance = gameObject.AddComponent<T>();
        return self.ToConstant(instance);
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindComponentOnNewGameObject<T>(this KernelBase self) 
        where T : Component
    {
        var bindingSyntax = BindComponentOnNewGameObject<T>(self, out _);
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
    // ReSharper disable once UnusedMethodReturnValue.Global
    public static Dictionary<Type, object> GetAll(this KernelBase self)
    {
        var bindingCacheFieldInfo = typeof(KernelBase).GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance);
        var bindings = bindingCacheFieldInfo?.GetValue(self) as Dictionary<Type, ICollection<IBinding>>;
        
        Dictionary<Type, object> resolved = new();
        bindings!.Map(binding => resolved.Add(binding.Key, self.Get(binding.Key)));
        return resolved;
    }

    // ReSharper disable once UnusedMember.Global
    public static void ResolveAll(this INinjectModule self)
    {
        var kernel = self.Kernel as KernelBase;
        kernel?.GetAll();
    }
}