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

    // ReSharper disable once MemberCanBeMadeStatic.Global
    public void Enable()
    {
        
    }

    public void LoadForContext(INinjectModule baseModule)
    {
        foreach (var bonejector in _bonejectors)
        {
            foreach (var set in bonejector.LoadSets)
            {
                if (!set.LoadFilter.ShouldLoad(baseModule)) continue;
                
                MelonLogger.Msg($"Loading {set.ModuleType.FullName} into {baseModule.GetType().FullName}");
                Kernel.Load(set.ModuleType, set.InitialParameters);
            }

            foreach (var instruction in bonejector.LoadInstructions)
            {
                if (instruction.baseModule == baseModule.GetType())
                    instruction.onLoad(Kernel);
            }
        }
        
        
    }

    public void Disable()
    {
        _kernel?.Dispose(true);
        _kernel = null;
    }
}