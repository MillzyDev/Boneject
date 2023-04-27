using System;

namespace Boneject.Ninject;

[Flags]
public enum Context
{
    None = 0,

    App = 1,

    Loading = 2,
    
    Bonelab = 4,

    Campaign = 8,
    
    EmptyGround = 16,
    
    Hub = 32,
    
    Intro = 64,
    
    Startup = 128,
    
    VoidG114 = 256,
    
    Player = 512
}