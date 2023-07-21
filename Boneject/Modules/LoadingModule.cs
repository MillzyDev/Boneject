using MelonLoader;
using Ninject.Modules;

namespace Boneject.Modules
{
    public class LoadingModule : NinjectModule
    {
        public override void Load()
        {
            MelonLogger.Msg("Loaded LoadingModule.");

        }
    }
}
