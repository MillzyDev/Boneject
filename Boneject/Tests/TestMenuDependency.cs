#if ENABLE_TESTS
using Il2CppSLZ.Bonelab;
using MelonLoader;
using Ninject;

namespace Boneject.Tests;

public class TestMenuDependency : IInitializable
{
    private readonly GameControl_startup _startup;
    private readonly TestAppDependency _appDependency;
    
    [Inject]
    private TestMenuDependency(GameControl_startup startup, TestAppDependency appDependency)
    {
        _startup = startup;
        _appDependency = appDependency;
    }
    
    public void Initialize()
    {
        MelonLogger.Msg($"Value of startup GameControl is: {_startup}");
        MelonLogger.Msg(_appDependency.str);
    }
}
#endif