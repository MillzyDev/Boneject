using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Boneject.Ninject.Modules;
using MelonLoader;
using Ninject;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;

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

    private BonejectKernel? _kernel;
    private Dictionary<Type, IBinding[]>? _bindingCache;
    private IBinding[]? _preservedBindings;
    private INinjectModule[]? _preservedModules;
    private readonly HashSet<Bonejector> _bonejectors = new();

    internal BonejectKernel Kernel
    {
        get
        {
            if (_kernel is not null) return _kernel;
            
            _kernel = new BonejectKernel(_ninjectSettings);
            _kernel.Load(new AppModule(this));

            _preservedModules = _kernel.GetModules().ToArray();
            
            _bindingCache = typeof(BonejectKernel)
                .GetField("bindingCache", BindingFlags.Instance | BindingFlags.NonPublic)
                ?.GetValue(_kernel) as Dictionary<Type, IBinding[]>;

            _preservedBindings = _bindingCache?.SelectMany(cache => cache.Value).ToArray();
            
            return _kernel;
        }
    }

    internal Dictionary<Type, IBinding[]> BindingCache => _bindingCache ?? new Dictionary<Type, IBinding[]>();
    internal IEnumerable<INinjectModule> PreservedModules => _preservedModules ?? Array.Empty<INinjectModule>();
    internal IEnumerable<IBinding> PreservedBindings => _preservedBindings ?? Array.Empty<IBinding>();

    public void Add(Bonejector bonejector) => _bonejectors.Add(bonejector);

    public void Enable()
    {
        
    }

    public void LoadForContext(INinjectModule baseModule)
    {
        foreach (var set in _bonejectors
                     .SelectMany(bonejector => bonejector.LoadSets, (bonejector, set) => new { bonejector, set })
                     .Where(t => t.set.LoadFilter.ShouldLoad(baseModule))
                     .Select(t => t.set))
        {
            MelonLogger.Msg($"Loading: {set.ModuleType.FullName} into {baseModule.GetType().Name}...");
            Kernel.Load(set.ModuleType, set.InitialParameters);
        }
    }

    public void Disable()
    {
        _kernel?.Dispose(true);
        _kernel = null;
    }
}