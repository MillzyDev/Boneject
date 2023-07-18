using System;

namespace Boneject
{
    [Flags]
    public enum Location
    {
        None = 0,
        
        App = 1,
        
        Bootstrapper = 2,
        
        Loading = 4,
        
        Player = 8,
        
        Startup = 16,
        
        VoidG114Menu = 32,
        
        Menu = Startup | VoidG114Menu
    }
}
