using System;

namespace Boneject;

[Flags]
public enum Context
{
    /// <summary>
    /// Indicates no context.
    /// </summary>
    None = 0,
    
    /// <summary>
    /// Indicates modules should be loaded on application start (when the first Start() method runs).
    /// All dependencies bound in this context will be available in all other contexts.
    /// </summary>
    App = 1,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.UI.LoadingScene"/>.
    /// The loading screen between levels.
    /// </summary>
    Loading = 2,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_Hub"/>.
    /// The "02 - BONELAB Hub" level.
    /// </summary>
    Hub = 4,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.BonelabGameControl"/>.
    /// This should be every level, except the 2 main menu levels and the hub.
    /// </summary>
    Game = 8,

    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_startup"/>.
    /// The first main menu scene.
    /// </summary>
    MenuStartup = 16,
    
    /// <summary>
    /// Indicates modules should be loaded at <see cref="SLZ.Bonelab.GameControl_MenuVoidG114.Start()"/>.
    /// The last main menu scene.
    /// </summary>
    MenuVoidG114 = 32,
}