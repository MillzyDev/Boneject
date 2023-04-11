using System.Reflection;
using MelonLoader;
using Ninject;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using Ninject.Syntax;
using UnityEngine;

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
        var bindingCacheFieldInfo = typeof(KernelBase).GetField("bindingCache", BindingFlags.NonPublic | BindingFlags.Instance);
        var bindingCache = bindingCacheFieldInfo?.GetValue(self) as Dictionary<Type, IBinding[]>;
        return bindingCache?.ToDictionary(binding => binding.Key, binding => self.Get(binding.Key));
    }

    public static void ResolveAll(this INinjectModule self)
    {
        var kernel = self.Kernel as KernelBase;
        kernel?.GetAll();
    }
}