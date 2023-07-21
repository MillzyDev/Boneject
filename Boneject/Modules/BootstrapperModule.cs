using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class BootstrapperModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded BootstrapperModule.");
        }
    }
}
