using System;

namespace Boneject.Ninject;

[Flags]
public enum Context
{
    None = 0,

    App = 1,

    Loading = 2,

    Campaign = 4,

    Hub = 8,

    Startup = 16,
    
    VoidG114 = 32,
    
    Player = 64
}