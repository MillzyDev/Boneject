using System.Reflection;
using MelonLoader;
using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boneject;

public static class NinjectExtensions
{
    public static IBindingWhenInNamedWithOrOnSyntax<T> AsComponentOnNewGameObject<T>(this IBindingToSyntax<T> self) 
        where T : Component
    {
        var go = new GameObject(typeof(T).FullName);
        var instance = go.AddComponent<T>();
        return self.ToConstant(instance);
    }

    public static IBindingWhenInNamedWithOrOnSyntax<T> AsComponentOnExistingGameObject<T>(
        this IBindingToSyntax<T> self, GameObject gameObject) where T : Component
    {
        var instance = gameObject.AddComponent<T>();
        return self.ToConstant(instance);
    }

    public static Dictionary<Type, object>? GetAll(this KernelBase self)
    {
        // We have to get all the binding types from the container, there are no public methods to do this,
        // so we use reflection to get all the bindings and their associated types.
        var bindingCacheFieldInfo = typeof(KernelBase).GetField("bindingCache", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
        var bindingCache = bindingCacheFieldInfo?.GetValue(self) as Dictionary<Type, IBinding[]>;
        MelonLogger.Msg(bindingCache == null); 
        
        // Create a dictionary of types and their resolved instances.
        return bindingCache?.ToDictionary(binding => binding.Key, binding => self.Get(binding.Key));
    }

    public static void ResolveAll(this INinjectModule self)
    {
        var kernel = self.Kernel as KernelBase;
        kernel?.GetAll();
    }
}