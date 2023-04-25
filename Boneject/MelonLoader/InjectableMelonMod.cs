using MelonLoader;

namespace Boneject.MelonLoader;

public abstract class InjectableMelonMod : MelonMod
{
    public new void OnInitializeMelon() => ModInitInjector.Inject(this);
}