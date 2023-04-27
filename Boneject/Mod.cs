using System.Reflection;
using Boneject.MelonLoader;
using Boneject.Ninject;
using MelonLoader;

namespace Boneject;

public static class BuildInfo
{
    public const string id = "dev.millzy.boneject";
    public const string name = "Boneject";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "1.0.0";
    public const string downloadLink = null!;
}

public class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        ModInitInjector.AddInjector(typeof(Bonejector), ConstructBonejector);
    }

    private static object ConstructBonejector(object? previous, ParameterInfo _, MelonInfoAttribute info)
    {
        if (previous is not null)
            return previous;

        // TODO: Register bonejector
        Bonejector bonejector = new(info);
        return bonejector;
    }
}