using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded AppModule.");
        }
    }
}
