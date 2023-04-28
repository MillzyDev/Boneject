using MelonLoader;

namespace Boneject.Tests;

public class TestDependency
{
    public TestDependency(object mod)
    {
        MelonLogger.Msg($"Loaded into ${mod.GetType().Name}");
    }

    public void Log(object obj)
    {
        MelonLogger.Msg($"Successfully injected into ${obj.GetType().Name}");
    }
}