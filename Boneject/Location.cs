namespace Boneject;

[Flags]
public enum Location
{
    None = 0,
    App = 1,
    StartupMenu = 2,
    VoidG114Menu = 4,
    Menu = StartupMenu | VoidG114Menu
}