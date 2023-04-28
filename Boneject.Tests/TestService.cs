using Ninject;

namespace Boneject.Tests;

public class TestService : IInitializable
{
    private readonly TestDependency _testDependency;

    [Inject]
    public TestService(TestDependency testDependency)
    {
        _testDependency = testDependency;
    }

    public void Initialize()
    {
        _testDependency.Log(this);
    }
}