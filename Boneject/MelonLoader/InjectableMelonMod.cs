using MelonLoader;

namespace Boneject.MelonLoader;

public abstract class InjectableMelonMod : MelonMod
{
    public override void OnInitializeMelon() => ModInitInjector.Inject(this);
}