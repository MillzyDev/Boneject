using MelonLoader;

namespace Boneject.Tests;

public abstract class TestLogger
{
    public TestLogger(TestDependency dependency)
    {
        MelonLogger.Msg(TestDependency.str);
    }
}