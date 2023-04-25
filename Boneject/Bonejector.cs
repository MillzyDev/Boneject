using System;
using System.Collections.Generic;
using Boneject.Filters;
using Boneject.ModuleLoaders;
using Ninject.Modules;

namespace Boneject;

public sealed class Bonejector
{
    private static readonly Lazy<Bonejector> _lazy = new(() => new Bonejector());

    private readonly Dictionary<Type, HashSet<INinjectModule>> _modules = new();

    private BonejectKernel? _baseKernel;
    private BonejectKernel? _currentKernel;

    private readonly HashSet<InstallSet> _installSets = new();

    private Bonejector()
    {
        _currentKernel = null;
    }

    public static Bonejector Instance => _lazy.Value;

    internal IEnumerable<InstallSet> InstallSets => _installSets;

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

    public void Install<T>(Context context, params object[] parameters) where T : INinjectModule
    {
        var loaderTypes = LoadersForContext(context);
        IInstallFilter filter = new MultiTypedInstallFilter(loaderTypes);
        _installSets.Add(new InstallSet(typeof(T), filter, parameters.Length != 0 ? parameters : null));
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