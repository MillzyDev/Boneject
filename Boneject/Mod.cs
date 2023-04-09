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
#endif

        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnLateInitializeMelon()
    {
        LoggerInstance.Msg($"Loading modules for location: {InstallLocation.App}");
        var moduleLoader = new AppModuleLoader();
        // Add stuff here if needed later
        moduleLoader.BeginLoad();
    }
}