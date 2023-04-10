#if ENABLE_TESTS
using MelonLoader;
using Ninject.Modules;

namespace Boneject.Tests.Modules;

// ReSharper disable once ClassNeverInstantiated.Global
public class TestMenuModule : NinjectModule
{
    public override void Load()
    {
        MelonLogger.Msg("Loaded Module!");
        Bind<TestMenuDependency>().ToSelf().InSingletonScope();
        this.ResolveAll();
    }
}
#endif