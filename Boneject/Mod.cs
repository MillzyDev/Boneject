using MelonLoader;

namespace Boneject;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnSceneWasLoaded(int buildIndex, string sceneName)
    {
        MelonLogger.Msg($"{sceneName} was loaded. Setting CurrentKernel to null.");
        Bonejector.Instance.CurrentKernel = null;
    }
}