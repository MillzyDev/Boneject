using System;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace Boneject.Ninject.Extensions;

// ReSharper disable once UnusedType.Global
public static class UnityBindingExtensions
{
    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnNewGameObject<T>(this IKernel self)
        where T : MonoBehaviour
    {
        var gameObject = new GameObject(typeof(T).FullName);
        var monoBehaviour = gameObject.AddComponent<T>();
        monoBehaviour.enabled = false; // disabled to ensure that Start does not run early
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind<T>().ToConstant(monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnNewGameObject<T>(this INinjectModule self)
        where T : MonoBehaviour
        => BindMonoBehaviourOnNewGameObject<T>(self.Kernel!);

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnNewGameObject(this IKernel self,
        Type type)
    {
        if (!typeof(MonoBehaviour).IsAssignableFrom(type)) throw new BonejectException("Type is not a MonoBehaviour.");

        var gameObject = new GameObject(type.FullName);
        var monoBehaviour = gameObject.AddComponent(Il2CppType.From(type)) as MonoBehaviour;
        monoBehaviour!.enabled = false;
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind(type).ToConstant((object)monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnNewGameObject(this INinjectModule self,
        Type type)
        => BindMonoBehaviourOnNewGameObject(self.Kernel!, type);
    
    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnNewGameObject<T>(this IKernel self, out GameObject gameObject)
        where T : MonoBehaviour
    {
        gameObject = new GameObject(typeof(T).FullName);
        var monoBehaviour = gameObject.AddComponent<T>();
        monoBehaviour.enabled = false;
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind<T>().ToConstant(monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnNewGameObject<T>(this INinjectModule self, out GameObject gameObject)
        where T : MonoBehaviour
        => BindMonoBehaviourOnNewGameObject<T>(self.Kernel!, out gameObject);

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnNewGameObject(this IKernel self,
        Type type, out GameObject gameObject)
    {
        if (!typeof(MonoBehaviour).IsAssignableFrom(type)) throw new BonejectException("Type is not a MonoBehaviour.");

        gameObject = new GameObject(type.FullName);
        var monoBehaviour = gameObject.AddComponent(Il2CppType.From(type)) as MonoBehaviour;
        monoBehaviour!.enabled = false;
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind(type).ToConstant((object)monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnNewGameObject(this INinjectModule self,
        Type type, out GameObject gameObject)
        => BindMonoBehaviourOnNewGameObject(self.Kernel!, type, out gameObject);

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnGameObject<T>(this IKernel self,
        GameObject gameObject) where T : MonoBehaviour
    {
        var monoBehaviour = gameObject.AddComponent<T>();
        monoBehaviour.enabled = false;
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind<T>().ToConstant(monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<T> BindMonoBehaviourOnGameObject<T>(this INinjectModule self,
        GameObject gameObject) where T : MonoBehaviour
        => BindMonoBehaviourOnGameObject<T>(self.Kernel!, gameObject);

    // ReSharper disable once MemberCanBePrivate.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnGameObject(this IKernel self, Type type,
        GameObject gameObject)
    {
        if (!typeof(MonoBehaviour).IsAssignableFrom(type)) throw new BonejectException("Type is not a MonoBehaviour.");
        
        var monoBehaviour = gameObject.AddComponent(Il2CppType.From(type)) as MonoBehaviour;
        monoBehaviour!.enabled = false;
        self.Inject(monoBehaviour);
        var bindingSyntax = self.Bind(type).ToConstant((object)monoBehaviour);
        monoBehaviour.enabled = true;
        return bindingSyntax;
    }

    // ReSharper disable once UnusedMember.Global
    public static IBindingWhenInNamedWithOrOnSyntax<object> BindMonoBehaviourOnGameObject(this INinjectModule self,
        Type type, GameObject gameObject)
        => BindMonoBehaviourOnGameObject(self.Kernel!, type, gameObject);
}