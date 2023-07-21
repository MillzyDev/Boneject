using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class StartupModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded StartupModule.");
        }
    }
}
