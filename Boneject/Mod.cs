using Boneject.ModuleLoaders;
using Boneject.Tests.Modules;
using MelonLoader;

namespace Boneject;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        var bonejector = Bonejector.Instance;
        bonejector.InstallModule<TestAppModule>(InstallLocation.App);

        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnLateInitializeMelon()
    {
        LoggerInstance.Msg("Loading modules...");
        var moduleLoader = new AppModuleLoader();
        // Add stuff here if needed later
        moduleLoader.BeginLoad();
    }
}