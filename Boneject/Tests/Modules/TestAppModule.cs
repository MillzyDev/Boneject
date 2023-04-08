using Ninject.Modules;

namespace Boneject.Tests.Modules;

public abstract class TestAppModule : NinjectModule
{
    public override void Load()
    {
        Bind<TestDependency>().ToSelf().InSingletonScope();
        Bind<TestLogger>().ToSelf().InSingletonScope();
    }
}