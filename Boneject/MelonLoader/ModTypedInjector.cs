using MelonLoader;

namespace Boneject.MelonLoader;

internal struct ModTypedInjector
{
    public readonly MelonInfoAttribute Info;
    public readonly TypedInjector TypedInjector;

    public ModTypedInjector(MelonInfoAttribute info, TypedInjector typedInjector)
    {
        Info = info;
        TypedInjector = typedInjector;
    }
}