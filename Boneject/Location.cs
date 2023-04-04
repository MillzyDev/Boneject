namespace Boneject;

[Flags]
public enum Location
{
    MenuStartup = 2,
    
    MenuVoidG114 = 4,
    
    Menu = MenuStartup | MenuVoidG114
}