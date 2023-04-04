namespace Boneject;

internal struct ModuleSet
{
    public readonly Type moduleType;
    public readonly object[]? initialParameters;

    public ModuleSet(Type moduleType, object[]? initialParameters = null)
    {
        this.moduleType = moduleType;
        this.initialParameters = initialParameters;
    }
}