using MelonLoader;

namespace Boneject.Tests;

public class TestLogger
{
    public TestLogger(TestDependency dependency)
    {
        MelonLogger.Msg(TestDependency.str);
    }
}