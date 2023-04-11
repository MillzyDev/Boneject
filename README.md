# Boneject
A BONELAB mod that provides a basic Ninject implementation and wrapper for use in the game.

This project is still in active development, as such the only available module install locations are App, MenuStartup and MenuVoidG114. More will be added in time, I would just rather iron out bugs before expanding it.

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
### 0.1.0
Initial Release.