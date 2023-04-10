#if ENABLE_TESTS
using MelonLoader;

namespace Boneject.Tests;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestAppLogger
{
    public TestAppLogger(TestAppDependency dependency)
    {
        MelonLogger.Msg(dependency.str);
        MelonLogger.Msg("If you are reading this, and you are not developing for Boneject, please inform Millzy; this build has the wrong config.");
    }
}
#endif