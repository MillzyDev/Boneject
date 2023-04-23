using MelonLoader;

namespace Boneject;

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg("Finished Initialisation!");
    }

    public override void OnSceneWasUnloaded(int buildIndex, string sceneName)
    {
        MelonLogger.Msg($"Scene unloaded. Setting CurrentKernel to null.");
        Bonejector.Instance.CurrentKernel = null;
    }
}