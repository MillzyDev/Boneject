#if ENABLE_TESTS
using MelonLoader;
using Ninject.Modules;

namespace Boneject.Tests.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestAppModule : NinjectModule
{
    public override void Load()
    {
        MelonLogger.Msg("Loaded module!");
        Bind<TestAppDependency>().ToSelf().InTransientScope();
        Bind<TestAppLogger>().ToSelf().InTransientScope();
    }
}
#endif