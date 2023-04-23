using System;
using System.Collections.Generic;
using Boneject.ModuleLoaders;
using Ninject;
using Ninject.Modules;

namespace Boneject;

public sealed class Bonejector
{
    private static readonly Lazy<Bonejector> _lazy = new(() => new Bonejector());

    private readonly Dictionary<Type, HashSet<INinjectModule>> _modules = new();

    private StandardKernel? _currentKernel;

    private Bonejector()
    {
        _currentKernel = null;
    }

    public static Bonejector Instance => _lazy.Value;

    internal StandardKernel? CurrentKernel
    {
        get => _currentKernel ??= new StandardKernel();
        set => _currentKernel = value;
    }

    public void InstallModule<T>(InstallLocation location) where T : INinjectModule
    {
        var module = Activator.CreateInstance<T>();

        var loaderTypes = LoadersForLocation(location);
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

    private static IEnumerable<Type> LoadersForLocation(InstallLocation location)
    {
        HashSet<Type> moduleLoaders = new();

        if (location.HasFlag(InstallLocation.Loading))
            moduleLoaders.Add(typeof(LoadingModuleLoader));
        if (location.HasFlag(InstallLocation.Game))
            moduleLoaders.Add(typeof(GameModuleLoader));
        if (location.HasFlag(InstallLocation.MenuStartup))
            moduleLoaders.Add(typeof(MenuStartupModuleLoader));
        if (location.HasFlag(InstallLocation.MenuVoidG114))
            moduleLoaders.Add(typeof(MenuVoidG114ModuleLoader));

        return moduleLoaders;
    }
}