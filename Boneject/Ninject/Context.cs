using System;

namespace Boneject.Ninject;

[Flags]
public enum Context
{
    None = 0,

    /// <summary>
    /// Loaded when the game runs it's first Start() function. Runs at the same time as OnLateInitializeMelon().
    /// All bindings here will be preserved in the subsequent contexts. No exposed types here by default./>
    /// </summary>
    App = 1,

    /// <summary>
    /// Loaded in all loading screens between levels.
    /// Exposed types: <see cref="SLZ.UI.LoadingScene"/>.
    /// </summary>
    Loading = 2,

    /// <summary>
    /// Loaded in all campaign levels, excluding the Hub level.
    /// Exposed types: <see cref="SLZ.Bonelab.BonelabInternalGameControl"/>, <see cref="SLZ.Rig.RigManager"/>,
    /// <see cref="SLZ.Bonelab.SaveFeatures"/>, <see cref="SLZ.SaveData.InventorySaveFilter"/>.
    /// </summary>
    Campaign = 4,

    /// <summary>
    /// Loaded in the BONELAB Hub level.
    /// Exposed types: <see cref="SLZ.Bonelab.GameControl_Hub"/>, <see cref="SLZ.Rig.RigManager"/>,
    /// <see cref="SLZ.Bonelab.Control_Player"/>, <see cref="SLZ.Bonelab.GauntletElevator"/>,
    /// <see cref="SLZ.SaveData.InventorySaveFilter"/>.
    /// </summary>
    Hub = 8,

    /// <summary>
    /// Loaded in the first Main Menu, before you finish the game.
    /// Exposed types: <see cref="SLZ.Bonelab.GameControl_startup"/>, <see cref="SLZ.Bonelab.Control_Player"/>,
    /// <see cref="SLZ.VRMK.BodyVitals"/>, <see cref="SLZ.UI.LaserCursor"/>.
    /// </summary>
    Startup = 16,
    
    /// <summary>
    /// Probably loaded sometime in the Home level. Keeping this here bc I dont wanna increment the major number.
    /// Exposed types: <see cref="SLZ.Bonelab.GameControl_VoidG114"/>, <see cref="SLZ.Rig.RigManager"/>.
    /// <see cref="SLZ.VRMK.BodyVitals"/>.
    /// </summary>
    [Obsolete]
    VoidG114 = 32,
    
    /// <summary>
    /// Loaded whenever the player's <see cref="SLZ.Rig.RigManager"/> is started.
    /// Exposed types: <see cref="SLZ.Rig.RigManager"/>.
    /// </summary>
    Player = 64,
    
    /// <summary>
    /// Loaded in the last main menu, after you finish the game.
    /// Exposed types: uhhhhhhhh
    /// </summary>
    VoidG114Menu = 128,
    
    Menu = Startup | VoidG114Menu 
}