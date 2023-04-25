using System;
using System.Collections.Generic;
using System.Reflection;
using Ninject;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using Ninject.Planning.Bindings;

namespace Boneject;

public static class NinjectExtensions
{
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