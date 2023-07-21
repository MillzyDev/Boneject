using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class PlayerModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded PlayerModule.");
        }
    }
}
