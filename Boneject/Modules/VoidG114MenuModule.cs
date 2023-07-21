using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class VoidG114MenuModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded VoidG114MenuModule.");
        }
    }
}
