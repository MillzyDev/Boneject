using System;
using System.Collections.Generic;
using MelonLoader;
using Ninject.Modules;

namespace Boneject.ModuleLoaders;

public class ModuleLoader<T> where T : ModuleLoader<T>
{
    private readonly Bonejector _bonejector;
    private readonly BonejectKernel _kernel;

    protected ModuleLoader()
    {
        _bonejector = Bonejector.Instance;
        _kernel = Bonejector.Instance.CurrentKernel!;
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public BonejectKernel Kernel => _kernel;

    internal void BeginLoad()
    {
        // MelonLogger.Msg("Registering modules...");
        // var modules = Bonejector.Instance.ModulesForLoader<T>();
        // _modules = _modules.Concat(modules).ToList();
        //
        // MelonLogger.Msg("Loading modules...");
        // Kernel?.Load(_modules.ToArray());
        // MelonLogger.Msg("Done!");

        MelonLogger.Msg($"Loading modules for {GetType().Name}...");
        
        var modules = new List<INinjectModule>();
        foreach (var set in _bonejector.InstallSets)
        {
            if (!set.installFilter.ShouldInstall(GetType())) continue;
            
            MelonLogger.Msg($"Loading: {set.moduleType}");
            var moduleInstance = (INinjectModule)Activator.CreateInstance(set.moduleType, set.initialParameters);
            modules.Add(moduleInstance);
        }
        Kernel.Load(modules);
        
        MelonLogger.Msg($"Finished loading modules for {GetType().Name}.");
    }
}