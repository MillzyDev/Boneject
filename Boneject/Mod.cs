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

        MelonEvents.OnApplicationStart.Subscribe(OnApplicationStart);
    }

    public override void OnDeinitializeMelon()
    {
        MelonEvents.OnApplicationStart.Unsubscribe(OnApplicationStart);
    }

    private new static void OnApplicationStart()
    {
        var moduleLoader = new AppModuleLoader();
        // Add stuff here if needed later
        moduleLoader.BeginLoad();
    }
}