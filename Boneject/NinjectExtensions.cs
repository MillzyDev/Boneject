using System.Reflection;
using Il2CppRootMotion.FinalIK;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure.Language;
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

    public static Dictionary<Type, object> GetAll(this KernelBase self)
    {
        var bindingCacheFieldInfo = typeof(KernelBase).GetField("bindings", BindingFlags.NonPublic | BindingFlags.Instance);
        var bindings = bindingCacheFieldInfo?.GetValue(self) as Dictionary<Type, ICollection<IBinding>>;
        
        Dictionary<Type, object> resolved = new();
        bindings!.Map(binding => resolved.Add(binding.Key, self.Get(binding.Key)));
        return resolved;
    }

    public static void ResolveAll(this INinjectModule self)
    {
        var kernel = self.Kernel as KernelBase;
        kernel?.GetAll();
    }
}