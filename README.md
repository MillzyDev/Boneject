# Boneject
A BONELAB mod that provides a basic Ninject implementation and wrapper for use in the game. This can be useful for mods that do a lot of things with a lot of concrete dependencies; Ninject can manage those for you.

This project is still in active development, there are not as many locations as there could be. More will be added in time or as I need them. If you'd like to use Boneject in a location that it currently does not support, please DM me on Discord (`Millzy#8418`) and I can add it for you. Or if you prefer, open a PR to add it in on the GitHub repository.

### This is installed, do I need it?
Probably! For the mods that do implement this, it will most likely be essential.

## For Developers
If you are considering implementing Boneject in to your mod, by all means, give it a go! I found that after using a Dependency Injection framework for modding Unity games (at the time Zenject in BeatSaber) it is very tricky to go back afterwards.

If you do not know how to use Boneject/Ninject or any Dependency Injection framework, I first highly recommend you to do some  on the subject. Personally, I recommend reading the [Extenject/Zenject README](https://github.com/Mathijs-Bakker/Extenject#what-is-dependency-injection) as it covers the basics of DI.

Read the wiki to get started!

## Credits
* [Auros](https://github.com/Auros) for creating [SiraUtil](https://github.com/Auros/SiraUtil), the inspiration for this project.

## Changelog
### v1.0.0
* Complete rewrite.
* Added the capability for mods to have dependencies injected into their entrypoints at `OnMelonInitialize`, through the use of the `InjectableMelonMod` class and the `Initialize` attribute. "Mod Initialization Dependencies" can be added using the `ModInitInjector.AddInjector()` static method. 

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