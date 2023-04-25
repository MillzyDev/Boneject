using System;
using System.Collections.Generic;
using Boneject.ModuleLoaders;
using Ninject.Modules;

namespace Boneject;

public sealed class Bonejector
{
    private static readonly Lazy<Bonejector> _lazy = new(() => new Bonejector());

    private readonly Dictionary<Type, HashSet<INinjectModule>> _modules = new();

    private BonejectKernel? _baseKernel;
    private BonejectKernel? _currentKernel;

    private Bonejector()
    {
        _currentKernel = null;
    }

    public static Bonejector Instance => _lazy.Value;

    internal BonejectKernel? BaseKernel
    {
        get => _baseKernel ??= new BonejectKernel();
        set => _baseKernel = value;
    }
    
    internal BonejectKernel? CurrentKernel
    {
        get => _currentKernel ??= BaseKernel!.DeepClone();
        set => _currentKernel = value;
    }

    public void InstallModule<T>(Context context) where T : INinjectModule
    {
        var module = Activator.CreateInstance<T>();

        var loaderTypes = LoadersForContext(context);
        foreach (var type in loaderTypes)
        {
            if (!_modules.ContainsKey(type))
                _modules.Add(type, new HashSet<INinjectModule>());
            _modules[type].Add(module);
        }
    }

    public void InstallModule<T>(Context context, params object[] args) where T : INinjectModule
    {
        var module = Activator.CreateInstance(typeof(T), args);
        
        var loaderTypes = LoadersForContext(context);
        foreach (var type in loaderTypes)
        {
            if (!_modules.ContainsKey(type))
                _modules.Add(type, new HashSet<INinjectModule>());
            _modules[type].Add(module);
        }
    }

    public IEnumerable<INinjectModule> ModulesForLoader<T>() where T : ModuleLoader<T>
    {
        return _modules.ContainsKey(typeof(T)) ? _modules[typeof(T)] : new();
    }

    private static IEnumerable<Type> LoadersForContext(Context context)
    {
        HashSet<Type> moduleLoaders = new();

        if (context.HasFlag(Context.App))
            moduleLoaders.Add(typeof(AppModuleLoader));
        if (context.HasFlag(Context.Loading))
            moduleLoaders.Add(typeof(LoadingModuleLoader));
        if (context.HasFlag(Context.Hub))
            moduleLoaders.Add(typeof(HubModuleLoader));
        if (context.HasFlag(Context.Game))
            moduleLoaders.Add(typeof(GameModuleLoader));
        if (context.HasFlag(Context.MenuStartup))
            moduleLoaders.Add(typeof(MenuStartupModuleLoader));
        if (context.HasFlag(Context.MenuVoidG114))
            moduleLoaders.Add(typeof(MenuVoidG114ModuleLoader));

        return moduleLoaders;
    }
}