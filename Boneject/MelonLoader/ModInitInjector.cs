using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;
using MelonLoader;
using Ninject;
using Ninject.Activation;
using Ninject.Parameters;

namespace Boneject.MelonLoader;

public static class ModInitInjector
{
    public delegate object? InjectParameter(object? previous, ParameterInfo? parameter, MelonInfoAttribute info);

    private static readonly INinjectSettings _ninjectSettings = new NinjectSettings
    {
        AllowNullInjection = true,
        MethodInjection = true,
        PropertyInjection = false,
        ThrowOnGetServiceNotFound = false,
#pragma warning disable CS0618
        InjectNonPublic = false,
#pragma warning restore CS0618
        InjectAttribute = typeof(OnInitializeAttribute),
        CachePruningInterval = default,
        DefaultScopeCallback = null,
        LoadExtensions = false,
        ExtensionSearchPatterns = new string[] {},
        UseReflectionBasedInjection = false,
        ActivationCacheDisabled = false
    };
    
    private static readonly Dictionary<TypedInjector, object?> _previousValues = new();
    private static readonly StandardKernel _kernel = new(_ninjectSettings);

    /// <summary>
    /// Registers a callback that is used to construct a dependency for each mod init.
    /// </summary>
    /// <param name="type">The type of the dependency you are constructing.</param>
    /// <param name="injector">The callback to construct the dependency.</param>
    public static void AddInjector(Type type, InjectParameter injector)
    {
        var typedInjector = new TypedInjector(type, injector);
        
        _kernel.Bind(typedInjector.Type)
            .ToMethod(ctx =>
            {
                var infoParam = ctx.Parameters.First(parameter => parameter.Name == "info");
                var info = infoParam.GetValue(ctx, ctx.Request.Target) as MelonInfoAttribute;
                
                var member = ctx.Request.Target?.Member;
                if (member is not MethodInfo method) return null;
                    
                var previous = _previousValues.ContainsKey(typedInjector) ? _previousValues[typedInjector] : null;

                var serviceType = ctx.Request.Service;

                var parameter = method.GetParameters()
                    .FirstOrDefault(parameter => parameter.ParameterType == serviceType);
                if (parameter == null) return null;

                var value = typedInjector.Inject(previous, parameter, info!);
                _previousValues.Add(typedInjector, value);
                return value;
            })
            .InTransientScope();
    }

    internal static void Inject(InjectableMelonMod mod)
    {
        _kernel.Inject(mod, new Parameter("info", mod.Info, true));
    }

    private readonly struct TypedInjector : IEquatable<TypedInjector>
    {
        public readonly Type Type;
        private readonly InjectParameter _injector;

        public TypedInjector(Type type, InjectParameter injector)
        {
            Type = type;
            _injector = injector;
        }

        public object? Inject(object? previous, ParameterInfo parameter, MelonInfoAttribute info)
             => _injector(previous, parameter, info);

        public bool Equals(TypedInjector other)
            => Type == other.Type && _injector == other._injector;

        public override bool Equals(object obj)
            => obj is TypedInjector other && Equals(other);

        public override int GetHashCode()
            => Type.GetHashCode() ^ _injector.GetHashCode();

        public static bool operator ==(TypedInjector left, TypedInjector right) => left.Equals(right);
        public static bool operator !=(TypedInjector left, TypedInjector right) => !left.Equals(right);
    }
}