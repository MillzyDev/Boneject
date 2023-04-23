using System;

namespace Boneject;

[Flags]
public enum InstallLocation
{
    None = 0,
    
    LoadingScreen = 1,
    
    GameControl = 2,

    MenuStartup = 4,
    
    MenuVoidG114 = 8,
    
    Menu = MenuStartup | MenuVoidG114
}