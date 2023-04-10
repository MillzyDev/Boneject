using Boneject.ModuleLoaders;
#if ENABLE_TESTS
using Boneject.Tests.Modules;
#endif
using MelonLoader;

namespace Boneject;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
#if ENABLE_TESTS
        var bonejector = Bonejector.Instance;
        bonejector.InstallModule<TestAppModule>(InstallLocation.App);
        bonejector.InstallModule<TestMenuModule>(InstallLocation.Menu);
#endif

        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnLateInitializeMelon()
    {
        LoggerInstance.Msg($"Loading modules for location: {InstallLocation.App}");
        var moduleLoader = new AppModuleLoader();
        // Add stuff here if needed later
        moduleLoader.BeginLoad();
        
        // Force resolve all dependencies
        var dependencies = moduleLoader.Kernel?.GetAll();
        if (dependencies == null) return;
        foreach (var dependency in dependencies)
            GlobalDependencies.AddDependency(dependency.Key, dependency.Value);
    }
}