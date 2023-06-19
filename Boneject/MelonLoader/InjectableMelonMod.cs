using MelonLoader;

namespace Boneject.MelonLoader
{
    /// <summary>
    /// A custom implementation of <see cref="MelonMod"/> where your initialize method is injected into with dependencies
    /// specified in other mods.
    /// </summary>
    public abstract class InjectableMelonMod : MelonMod
    {
        public override void OnInitializeMelon()
        {
            ModInitInjector.Inject(this);
        }
    }
}
