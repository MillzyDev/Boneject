﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Boneject.Attributes;
using MelonLoader;
using Ninject;
using Ninject.Parameters;

namespace Boneject
{
    public static class ModInitInjector
    {
        private static readonly INinjectSettings NinjectSettings = new NinjectSettings
        {
            AllowNullInjection = true,
            MethodInjection = true,
            PropertyInjection = false,
            ThrowOnGetServiceNotFound = false,
#pragma warning disable CS0618
            InjectNonPublic = false,
#pragma warning restore CS0618
            // use a custom attribute for readability and control over what members it can be associated with
            InjectAttribute = typeof(OnInitializeAttribute),
            CachePruningInterval = default,
            DefaultScopeCallback = null,
            LoadExtensions = false,
            ExtensionSearchPatterns = new string[] { },
            UseReflectionBasedInjection = false,
            ActivationCacheDisabled = false
        };

        // here in case mod disabling becomes a thing
        private static readonly Dictionary<ModTypedInjector, object?> PreviousValues = new();
        private static readonly StandardKernel Kernel = new(NinjectSettings);

        /// <summary>
        /// Registers a callback that is used to construct a dependency for each mod init.
        /// </summary>
        /// <param name="type">The type of the dependency you are constructing.</param>
        /// <param name="injector">The callback to construct the dependency.</param>
        public static void AddInjector(Type type, InjectParameter injector)
        {
            var typedInjector = new TypedInjector(type, injector);

            Kernel.Bind(typedInjector.Type)
                .ToMethod(ctx =>
                {
                    IParameter? infoParam = ctx.Parameters.First(parameter => parameter.Name == "info");
                    // need the melon info of whats getting injected for indexing reasons
                    var info = infoParam.GetValue(ctx, ctx.Request.Target) as MelonInfoAttribute;

                    MemberInfo? member = ctx.Request.Target?.Member;
                    if (member is not MethodInfo method) return null;

                    var modTypedInjector = new ModTypedInjector(info!, typedInjector);
                    object? previous = PreviousValues.TryGetValue(modTypedInjector, out object? previousValue)
                        ? previousValue
                        : null;

                    Type? serviceType = ctx.Request.Service;

                    ParameterInfo? parameter = method.GetParameters() // based on BSIPA's bit
                        .FirstOrDefault(parameter => parameter.ParameterType == serviceType);
                    if (parameter == null) return null;

                    object? value = typedInjector.Inject(previous, parameter, info!);
                    PreviousValues.Add(modTypedInjector, value);
                    return value;
                })
                .InTransientScope(); // ensure code above is run for each injection
        }

        internal static void Inject(InjectableMelonMod mod)
        {
            Kernel.Inject(mod, new Parameter("info", mod.Info, true));
        }
    }
}
