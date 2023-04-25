using Boneject.ModuleLoaders;
using MelonLoader;

namespace Boneject;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnLateInitializeMelon()
    {
        MelonLogger.Msg($"Loading modules for context: {Context.App}");
        Bonejector.Instance.CurrentKernel = null;
        Bonejector.Instance.BaseKernel = null; // Only happens once, ensures that our bindings stay global.
        var moduleLoader = new AppModuleLoader();
        moduleLoader.BeginLoad();
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        MelonLogger.Msg($"{sceneName} was loaded. Setting CurrentKernel to null.");
        Bonejector.Instance.CurrentKernel = null;
    }
}