using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Boneject.MelonLoader.Attributes;
using MelonLoader;
using Ninject;

namespace Boneject.MelonLoader;

public static class ModInitInjector
{
    public delegate object? InjectParameter(object? previous, ParameterInfo parameter, MelonInfoAttribute info);

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

    private static readonly HashSet<TypedInjector> _injectors = new();
    private static readonly Dictionary<TypedInjector, object?> _previousValues = new();

    public static void AddInjector(Type type, InjectParameter injector)
        => _injectors.Add(new TypedInjector(type, injector));

    public static void Inject(InjectableMelonMod mod)
    {
        var initMethods = mod.GetType().GetMethods()
            .Where(method => method.GetCustomAttributes(typeof(OnInitializeAttribute)).Any());
        var methodInfos = initMethods as MethodInfo[] ?? initMethods.ToArray();
        if (methodInfos.Length > 1)
            throw new BonejectException("Marking multiple methods with OnInitializeAttribute is not allowed.");
        var initMethod = methodInfos.First();

        var parameters = initMethod.GetParameters();
        var resolvedParameters = ResolveParameters(parameters, mod.Info);

        StandardKernel kernel = new(_ninjectSettings);
        foreach (var resolvedParameter in resolvedParameters)
        {
            kernel.Bind(resolvedParameter.GetType()).ToConstant(resolvedParameter).InSingletonScope();
        }
        
        kernel.Inject(mod);
    }

    private static IEnumerable<object> ResolveParameters(IEnumerable<ParameterInfo> parameters, MelonInfoAttribute info)
    {
        var resolvedValues = new HashSet<object>();
        foreach (var parameter in parameters)
        {
            var resolved = ResolveForParameter(parameter, info);
            if (resolved != null)
                resolvedValues.Add(resolved);
        }
        return resolvedValues;
    }

    private static object? ResolveForParameter(ParameterInfo parameter, MelonInfoAttribute info)
    {
        var parameterType = parameter.ParameterType;
        var value = parameterType.IsValueType ? Activator.CreateInstance(parameterType) : null;

        var toUse = _injectors
            .Select(injector => (inject: injector, priority: MatchPriority(parameterType, injector.Type)))
            .Where(tuple => tuple.priority != null)
            .Select(tuple => (tuple.inject, priority: tuple.priority!.Value))
            .OrderByDescending(tuple => tuple.priority)
            .Select(t => t.inject);

        foreach (var pair in toUse)
        {
            object? previous = null;
            if (_previousValues.ContainsKey(pair))
                previous = _previousValues[pair];

            var resolved = pair.Inject(previous, parameter, info);
            _previousValues[pair] = resolved;

            if (resolved == null) continue;
            value = resolved;
            break;
        }

        return value;
    }

    private static int? MatchPriority(Type target, Type source)
    {
        if (target == source) return int.MaxValue;
        if (!target.IsAssignableFrom(source)) return null;
        if (!target.IsInterface && !source.IsSubclassOf(target)) return int.MinValue;

        var value = int.MaxValue - 1;
        while (true)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (source is null) return value;
            if (source.IsInterface && !source.GetInterfaces().Contains(target)) return value;
            if (target == source) return value;
            
            value--;
            source = source.BaseType!;
        }
    }
    
    private struct TypedInjector : IEquatable<TypedInjector>
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