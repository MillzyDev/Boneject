using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;

namespace Boneject.Tests;

public static class BuildInfo
{
    public const string id = "dev.millzy.boneject.tests";
    public const string name = "Boneject Tests";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "1.0.0";
    public const string downloadLink = null!;
}

public class Mod : InjectableMelonMod
{
    [OnInitialize]
    // ReSharper disable once UnusedMember.Global
    public void OnInitializeMod(Bonejector bonejector)
    {
        LoggerInstance.Msg("Starting Boneject tests.");
        LoggerInstance.Msg($"Injected Bonejector instance is {NullString.Create(bonejector)}");
    }
}
