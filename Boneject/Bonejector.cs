using Boneject.ModuleLoaders;
using Ninject.Modules;

namespace Boneject;

public sealed class Bonejector
{
    private static readonly Lazy<Bonejector> Lazy = new(() => new Bonejector());

    private readonly Dictionary<Type, HashSet<INinjectModule>> _modules = new();

    private Bonejector()
    {
    }

    public static Bonejector Instance => Lazy.Value;

    public void InstallModule<T>(InstallLocation location) where T : INinjectModule
    {
        var module = Activator.CreateInstance<T>();
        var loaderTypes = LoadersForLocation(location);
        foreach (var type in loaderTypes)
        {
            if (_modules.ContainsKey(type))
                _modules.Add(type, new HashSet<INinjectModule>());
            _modules[type].Add(module);
        }
    }

    public HashSet<INinjectModule> ModulesForLoader<T>() where T : ModuleLoaders.ModuleLoader
    {
        return _modules[typeof(T)];
    }

    public HashSet<INinjectModule> ModulesForLoader(Type loaderType)
    {
        if (!typeof(ModuleLoaders.ModuleLoader).IsAssignableFrom(loaderType))
            throw new ArgumentException("loaderType is not assignable from Boneject.ModuleLoaders.ModuleLoader");
        return _modules[loaderType];
    }

    private IEnumerable<Type> LoadersForLocation(InstallLocation location)
    {
        HashSet<Type> moduleLoaders = new();

        if (location.HasFlag(InstallLocation.App))
            moduleLoaders.Add(typeof(AppModuleLoader));

        return moduleLoaders;
    }
}