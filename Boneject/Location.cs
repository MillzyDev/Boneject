namespace Boneject;

[Flags]
public enum Location
{
    None = 0,
    
    App = 1,
    
    MenuStartup = 2,
    
    MenuVoidG114 = 4,
    
    Menu = MenuStartup | MenuVoidG114
}