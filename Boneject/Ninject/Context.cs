using System;

namespace Boneject.Ninject;

[Flags]
public enum Context
{
    None = 0,

    App = 1,

    Loading = 2,

    Campaign = 4,
    
    EmptyGround = 8,
    
    Hub = 16,
    
    Intro = 32,
    
    Startup = 64,
    
    VoidG114 = 128,
    
    Player = 256
}