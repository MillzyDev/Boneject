using System;
using System.Collections.Generic;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;

namespace Boneject.Ninject;

public class BonejectManager
{
    private readonly NinjectSettings _ninjectSettings = new()
    {
        InjectAttribute = typeof(InjectAttribute),
        CachePruningInterval = TimeSpan.FromSeconds(30.0),
        DefaultScopeCallback = StandardScopeCallbacks.Transient,
        LoadExtensions = true,
        ExtensionSearchPatterns = new[] // Just in case someone decides to use these in the future
        {
            "Ninject.Extensions.*.dll",
            "Ninject.Web*.dll"
        },
        UseReflectionBasedInjection = false,
        ActivationCacheDisabled = false,
        AllowNullInjection = false,
        MethodInjection = true,
        PropertyInjection = true,
        ThrowOnGetServiceNotFound = false
    };

    private BonejectKernel? _originalKernel;
    private BonejectKernel? _currentKernel;
    private readonly HashSet<Bonejector> _bonejectors = new();

    internal BonejectKernel Kernel => _currentKernel ??= _originalKernel!.Clone();

    public void Add(Bonejector bonejector) => _bonejectors.Add(bonejector);

    public void Enable()
    {
        _originalKernel = new BonejectKernel(_ninjectSettings);
        _originalKernel.Load<AppModule>();
    }

    public void LoadForContext(INinjectModule baseModule)
    {
        foreach (var bonejector in _bonejectors)
        {
            // TODO: Exposing... somehow... probably just gonna do a Resources.GetObjectsOfTypeAll as much as I hate it.
            
            // Load Modules
            foreach (var set in bonejector.LoadSets)
            {
                if (!set.LoadFilter.ShouldLoad(baseModule)) continue;
                
                MelonLogger.Msg($"Loading: {set.ModuleType.FullName} into {baseModule.GetType().Name}...");
                Kernel.Load(set.ModuleType, set.InitialParameters);
            }
            
            // TODO: Instruction loading
        }
    }

    public void Disable()
    {
        DisposeCurrentKernel();
        _originalKernel?.Dispose(true);
        _originalKernel = null;
    }

    public void DisposeCurrentKernel()
    {
        _currentKernel?.Dispose(true);
        _currentKernel = null;
    }

    internal void ContextChanged()
    {
        DisposeCurrentKernel();
    }
}