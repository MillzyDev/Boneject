using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using Ninject;
using Ninject.Modules;

namespace Boneject.ModuleLoaders;

public class ModuleLoader<T> where T : ModuleLoader<T>
{
    private List<INinjectModule> _modules;

    protected ModuleLoader()
    {
        Kernel = Bonejector.Instance.CurrentKernel;
        _modules = new List<INinjectModule>();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public StandardKernel? Kernel { get; private set; }

    internal void EndKernel()
    {
        Kernel = null;
        Bonejector.Instance.CurrentKernel = null;
    }

    public void RegisterModule(INinjectModule module)
    {
        _modules.Add(module);
    }

    private void RegisterInstalledModules()
    {
        var modules = Bonejector.Instance.ModulesForLoader<T>();
        _modules = _modules.Concat(modules).ToList();
    }

    internal void BeginLoad()
    {
        MelonLogger.Msg("Loading global dependencies...");
        foreach (var dependency in GlobalDependencies.Get().Where(dependency => Kernel.TryGet(dependency.Key) == null))
        {
            MelonLogger.Msg($"Binding global dependency: {dependency.Key.FullName}");
            if (dependency.Value == null)
            {
                Kernel?.Bind(dependency.Key).ToSelf().InSingletonScope();
                MelonLogger.Msg($"No instance of {dependency.Key.FullName} available. Ninject will instantiate.");
                MelonLogger.Msg($"Value: {dependency.Value}");
            }
            else
            {
                Kernel?.Bind(dependency.Key).ToConstant(dependency.Value).InSingletonScope();
            }
        }

        MelonLogger.Msg("Registering modules...");
        RegisterInstalledModules();

        MelonLogger.Msg("Loading modules...");
        Kernel?.Load(_modules.ToArray());
        MelonLogger.Msg("Done!");
    }
}