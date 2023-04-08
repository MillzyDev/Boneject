#if ENABLE_TESTS
using MelonLoader;
using Ninject;
using Ninject.Modules;

namespace Boneject.Tests.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestAppModule : NinjectModule
{
    public override void Load()
    {
        MelonLogger.Msg("Loaded module!");
        Bind<TestDependency>().ToSelf().InSingletonScope();
        Bind<TestLogger>().ToSelf().InSingletonScope();
        _ = Kernel?.Get<TestLogger>();
    }
}
#endif