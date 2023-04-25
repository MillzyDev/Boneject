using Ninject;
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
}