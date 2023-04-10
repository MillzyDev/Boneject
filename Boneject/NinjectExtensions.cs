using System.Reflection;
using Ninject;
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

    public static Dictionary<Type, object> GetAll(this StandardKernel self)
    {
        // We have to get all the binding types from the container, there are no public methods to do this,
        // so we use reflection to get all the bindings and their associated types.
        var bindingCacheFieldInfo = typeof(StandardKernel).GetField("bindingCache", BindingFlags.NonPublic | BindingFlags.Instance);
        var bindingCache = (bindingCacheFieldInfo?.GetValue(self) as Dictionary<Type, IBinding[]>)!;
        
        // Create a dictionary of types and their resolved instances.
        return bindingCache.ToDictionary(binding => binding.Key, binding => self.Get(binding.Key));
    }
}