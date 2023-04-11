using MelonLoader;
using Ninject;
using Ninject.Modules;

namespace Boneject.ModuleLoaders;

public class ModuleLoader<T> where T : ModuleLoader<T>
{
    private List<INinjectModule> _modules;

    protected ModuleLoader()
    {
        _modules = new List<INinjectModule>();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public StandardKernel? Kernel { get; private set; }

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
        Kernel = new StandardKernel();


        MelonLogger.Msg("Loading global dependencies...");
        foreach (var dependency in GlobalDependencies.Get())
        {
            MelonLogger.Msg($"Binding global dependency: {dependency.Key.FullName}");
            if (Kernel.TryGet(dependency.Key) != null) continue;
            
            if (dependency.Value == null)
                Kernel.Bind(dependency.Key).ToSelf().InSingletonScope();
            else
                Kernel.Bind(dependency.Key).ToConstant(dependency.Value).InSingletonScope();
        }

        MelonLogger.Msg("Registering modules...");
        RegisterInstalledModules();

        MelonLogger.Msg("Loading modules...");
        Kernel.Load(_modules.ToArray());
        MelonLogger.Msg("Done!");
    }
}