using Ninject;
using Ninject.Modules;

namespace Boneject.Tests.Modules;

public class AppModule : NinjectModule
{
    private readonly TestDependency _testDependency;
    
    public AppModule()
    {
        _testDependency = new TestDependency(this);
    }
    
    public override void Load()
    {
        Bind<TestDependency>().ToConstant(_testDependency).InSingletonScope();
        Bind<TestService>().ToSelf().InSingletonScope();
        Kernel?.Get<TestService>(); // Force resolve
    }
}