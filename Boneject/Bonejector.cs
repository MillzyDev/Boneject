using Boneject.ModuleLoaders;
using Ninject.Modules;

namespace Boneject;

public sealed class Bonejector
{
    private static readonly Lazy<Bonejector> _lazy = new(() => new Bonejector());

    private readonly Dictionary<Type, HashSet<INinjectModule>> _modules = new();

    private Bonejector()
    {
    }

    public static Bonejector Instance => _lazy.Value;

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

        if (location.HasFlag(InstallLocation.App))
            moduleLoaders.Add(typeof(AppModuleLoader));
        if (location.HasFlag(InstallLocation.MenuStartup))
            moduleLoaders.Add(typeof(MenuStartupModuleLoader));
        if (location.HasFlag(InstallLocation.MenuVoidG114))
            moduleLoaders.Add(typeof(MenuVoidG114ModuleLoader));

        return moduleLoaders;
    }
}