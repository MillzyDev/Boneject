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
}