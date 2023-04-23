# Boneject
A BONELAB mod that provides a basic Ninject implementation and wrapper for use in the game. This can be useful for mods that do a lot of things with a lot of concrete dependencies; Ninject can manage those for you.

This project is still in active development, there are not as many locations as there could be. More will be added in time or as I need them. If you'd like to use Boneject in a location that it currently does not support, please DM me on Discord (`Millzy#8418`) and I can add it for you. Or if you prefer, open a PR to add it in on the GitHub repository.

### This is installed, do I need it?
Probably! For the mods that do implement this, it will most likely be essential.

## For Developers
If you are considering implementing Boneject in to your mod, by all means, give it a go! I found that after using a Dependency Injection framework for modding Unity games (at the time Zenject in BeatSaber) it is very tricky to go back afterwards.

If you do not know how to use Boneject/Ninject or any Dependency Injection framework, I first highly recommend you to do some  on the subject. Personally, I recommend reading the [Extenject/Zenject README](https://github.com/Mathijs-Bakker/Extenject#what-is-dependency-injection) as it covers the basics of DI.

### Introduction to the Boneject API
If you are already familiar with Ninject, great! If you are not, I recommend you read some documentation for it; since Boneject builds on top of Ninject to allow it to function within the game. NOTE: Boneject is by no means perfect, a lot of things could certainly be done better; If there are some parts of Boneject that are bad or could be done better, please open a PR.

Anyway, back to the API

#### 1: Add References
Add references to both `/Mods/Boneject.dll` and `/UserLibs/Ninject.dll`.

#### 2: Creating a Module
Boneject requires the use of [Ninject Modules](https://github.com/ninject/Ninject/wiki/Modules-and-the-Kernel#modules) (as of current). For this example, lets create a way to access the menu music ZoneMusic in the startup menu using Boneject.

First create a new class, I recommend naming it `MenuModule` or similar. Make sure the class inherits from `Ninject.Modules.NinjectModule` and implement the `Load` method. Your class should look like this:
```csharp
using Ninject.Modules;

public class MenuModule : NinjectModule 
{
    public override void Load()
    {
    
    }
}
```
We will revisit this class later.

#### Creating a Service
A service is a class that we will "bind" when the module is loaded. But first we need to create it. For this demonstration, we will use a MonoBehaviour for this service, since this is probably more likely to receive use in modding. Let's call this, `AudioReader`:
```csharp
using UnityEngine;

public class AudioReader : MonoBehaviour
{
}
```
Since Unity handles the MonoBehaviour's construction we cannot use constructor injection for supplying the `GameControl_startup` dependency. Ideally, we should also avoid field injection so that leaves us with two (2) options: Method Injection or Property Injection. For this example, we will use Method Injection. Create a method called `Inject` or similar and mark it with the `Ninject.Inject` attribute.
```csharp
using Ninject;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    [Inject]
    public void Inject()
    {
    }
}
```
The reason that Inject is public here is because Ninject sometimes has issue dealing with injecting into private members. (Although this is probably something I could fix in the Ninject Settings somewhere)

In order for `GameControl_startup` to be injected into the method, it must be a parameter of said method. Once the module is loaded, Ninject will automatically run the method and inject it with any bound dependencies. We will also assign the parameter
```csharp
using Ninject;
using SLZ.Bonelab;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    [Inject]
    public void Inject(GameControl_startup gameControl)
    {
    }
}
```
Now we can get the menu music ZoneMusic.
```csharp
using Ninject;
using SLZ.Bonelab;
using SLZ.SFX;
using UnityEngine;

public class AudioReader : MonoBehaviour
{
    [Inject]
    public void Inject(GameControl_startup gameControl)
    {
        ZoneMusic menuMusic = gameControl.music_menu;
    }
}
```
And there we go. But wait, we still need to make it so that Ninject can actually inject GameControl_startup and we also need to register the module in Boneject in order for it to be loaded.

#### Binding our Service
We use to module for binding our service. Since our service is a MonoBehaviour we need to use the `AsComponentOnNewGameObject` or the `AsComponentOnExistingGameObject` extension methods. Since we do not already have a game object to add the behaviour to, we will use the former. Bind your behaviour in your module by doing the following.
```csharp
using Ninject.Modules;

public class MenuModule : NinjectModule
{
    public override void Load()
    {
        Bind<AudioReader>().AsComponentOnNewGameObject().InSingletonScope();
        // InSingletonScope ensures that we will only ever have one instance of AudioReader bound.
    }
}
```
Now that we have bound our service, we need to tell Bonelab where to load the module.
#### Installing our Module
In your base class that implements `MelonMod`, in any method that runs before `OnLateInitializeMelon()`, first get an instance of the `Bonejector` singleton.
```csharp
using Boneject;
using MelonLoader;

public class Mod : MelonMod 
{
    public override void OnInitializeMelon()
    {
        Bonejector bonejector = Bonejector.Instance;
    }
}
```
Once you have done that, we can install our module to the startup menu location using the `InstallModule` method and the `InstallLocation` enum.
```csharp
using Boneject;
using MelonLoader;

public class Mod : MelonMod 
{
    public override void OnInitializeMelon()
    {
        Bonejector bonejector = Bonejector.Instance;
        bonejector.InstallModule<MenuModule>(InstallLocation.MenuStartup);
    }
}
```
For more information on InstallLocations and where they install to, please read the documentation in the `InstallLocations.cs` file.

And that's it! This was a very simple example, but the uses are relatively vast. If you have any question/concerns contact `Millzy#8418` on Discord.

## Changelog
### v0.3.0
* Removed `AsComponentOnNewGameObject<T>` and `AsComponentOnExistingGameObject<T>` extension methods due to Ninject not injecting into constant bindings and not being able to access the Kernel to fix it.
* Added `BindComponentOnNewGameObject<T>` and `BindComponentOnExistingGameObject<T>` extension methods to replace the aforementioned.

### v0.2.0
* Removed `App` location and its associated ModuleLoader and HarmonyPatch. This is due to the behaviour being different than desired. GlobalDependencies must now be added manually using the `GlobalDependencies` static class. Ninject can still handle their creation however.
* Added a `Game` location, that should load in every level, except the two main menu levels. An instance of `BonelabGameControl` is automatically loaded into the Ninject Kernel before the modules are loaded.
* Added a `Loading` location, its the loading screen basically. An instance of `LoadingScene` is automatically loaded into the Ninject Kernel before modules are loaded.
* Added more logging messages.
* Made it so that instances of the Ninject Kernel are limited to one per scene (in theory). Should help with compatibility with some future things.
* Fixed a bug (logic error) where the Ninject Kernel would only be initialized when modules are loaded, preventing "automatically bound dependencies" from actually being bound to the Ninject Kernel.
* Fixed a bug (another logic error) where global dependencies that are created by Ninject would not be updated in the global dependency dictionary. This may have been the problem causing the first issue, I can add the App location back if requested.

### v0.1.0
* Initial Release.