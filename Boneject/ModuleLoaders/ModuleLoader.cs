using System.Collections.Generic;
using System.Linq;
using MelonLoader;
using Ninject.Modules;

namespace Boneject.ModuleLoaders;

public class ModuleLoader<T> where T : ModuleLoader<T>
{
    private List<INinjectModule> _modules;
    private BonejectKernel? _kernel;

    protected ModuleLoader()
    {
        Kernel = Bonejector.Instance.CurrentKernel;
        _modules = new List<INinjectModule>();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public BonejectKernel? Kernel { get; }

    internal void BeginLoad()
    {
        MelonLogger.Msg("Registering modules...");
        var modules = Bonejector.Instance.ModulesForLoader<T>();
        _modules = _modules.Concat(modules).ToList();

        MelonLogger.Msg("Loading modules...");
        Kernel?.Load(_modules.ToArray());
        MelonLogger.Msg("Done!");
    }
}