using System;

namespace Boneject;

[Flags]
public enum InstallLocation
{
    /// <summary>
    /// Indicates no location.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// 
    /// </summary>
    App = 1,
    
    /// <summary>
    /// Indicated modules should be loaded at <see cref="SLZ.UI.LoadingScene.Start()"/>. The loading screen between levels.
    /// </summary>
    Loading = 2,
    
    /// <summary>
    /// 
    /// </summary>
    Hub = 4,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.BonelabGameControl.Start()"/>. This should be every level, except the 2 main menu levels.
    /// </summary>
    Game = 8,

    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_startup.Start()"/>. The first main menu scene.
    /// </summary>
    MenuStartup = 16,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_MenuVoidG114.Start()"/>. The last main menu scene.
    /// </summary>
    MenuVoidG114 = 32,
}