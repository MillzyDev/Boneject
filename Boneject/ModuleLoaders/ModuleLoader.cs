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

    public void BeginLoad()
    {
        Kernel = new StandardKernel();

        foreach (var dependency in GlobalDependencies.Get())
        {
            if (dependency.Value == null)
                Kernel.Bind(dependency.Key).ToSelf().InSingletonScope();
            else
                Kernel.Bind(dependency.Key).ToConstant(dependency.Value).InSingletonScope();
        }
        
        RegisterInstalledModules();
        
        Kernel.Load(_modules.ToArray());
    }
}