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
        LoggerInstance.Msg("Finished Initialisation!");
    }
}