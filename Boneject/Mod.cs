using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using MelonLoader;

namespace Boneject;

public static class BuildInfo
{
    public const string name = "Boneject";
    public const string author = "Millzy";
    public const string company = null!;
    public const string version = "1.0.0";
    public const string downloadLink = null!;
}

public class Mod : InjectableMelonMod
{
    [OnInitialize]
    void OnInitializeMod()
    {
        
    }
}