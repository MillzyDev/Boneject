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
    /// Indicated modules should be loaded at <see cref="SLZ.UI.LoadingScene.Start()"/>. The loading screen between levels.
    /// </summary>
    Loading = 1,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.BonelabGameControl.Start()"/>. This should be every level, except the 2 main menu levels.
    /// </summary>
    Game = 2,

    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_startup.Start()"/>. The first main menu scene.
    /// </summary>
    MenuStartup = 4,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_MenuVoidG114.Start()"/>. The last main menu scene.
    /// </summary>
    MenuVoidG114 = 8,
}