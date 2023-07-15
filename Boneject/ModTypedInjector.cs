using MelonLoader;

namespace Boneject
{
    internal struct ModTypedInjector
    {
        // ReSharper disable once NotAccessedField.Global
        public readonly MelonInfoAttribute Info;

        // ReSharper disable once NotAccessedField.Global
        public readonly TypedInjector TypedInjector;

        public ModTypedInjector(MelonInfoAttribute info, TypedInjector typedInjector)
        {
            Info = info;
            TypedInjector = typedInjector;
        }
    }
}
