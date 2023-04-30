# Boneject
[![](https://img.shields.io/badge/Source-Boneject-informational?style=for-the-badge&logo=GitHub)](https://github.com/MillzyDev/Boneject)
[![](https://img.shields.io/github/v/release/MillzyDev/Boneject?style=for-the-badge)](https://github.com/MillzyDev/Boneject/releases/latest)
[![](https://img.shields.io/github/license/MillzyDev/Boneject?style=for-the-badge)](https://github.com/MillzyDev/Boneject/blob/master/LICENSE)
[![](https://img.shields.io/badge/Donate-Ko--fi-FF5E5B?style=for-the-badge&logo=Ko-fi)](https://ko-fi.com/millzy)
[![](https://img.shields.io/badge/dynamic/json?color=informational&label=Downloads&query=%24.total_downloads&suffix=%20&url=https%3A%2F%2Fthunderstore.io%2Fapi%2Fexperimental%2Fpackage%2FMillzy%2FBoneject%2F&style=for-the-badge)](https://github.com/MillzyDev/Boneject)

A BONELAB mod that provides a basic Ninject implementation and wrapper for use in the game. This can be useful for mods that do a lot of things with a lot of concrete dependencies; Ninject can manage those for you.

This project is still in active development, there are not as many locations as there could be. More will be added in time or as I need them. If you'd like to use Boneject in a location that it currently does not support, please DM me on Discord (`Millzy#8418`) and I can add it for you. Or if you prefer, open a PR to add it in on the GitHub repository.

### This is installed, do I need it?
Probably! For the mods that do implement this, it will most likely be essential.

## For Contributors
1. Clone the project from the [GitHub repository](https://github.com/MillzyDev/Boneject).
2. Open `Boneject.sln` in your favourite IDE.
3. Create a copy of `Boneject.csproj.user.template` and remove the `.template` suffix.
4. In the same file, replace the comment between the `<BonelabDir>` tags with your BONELAB installation path. This will let Boneject build with your game's assemblies.
5. Save the file.
6. Make sure Ninject has been downloaded from Nuget.
7. All set!

## For Developers
If you are considering implementing Boneject in to your mod, by all means, give it a go! I found that after using a Dependency Injection framework for modding Unity games (at the time Zenject in BeatSaber) it is very tricky to go back afterwards.

[Read the wiki to get started.](https://github.com/MillzyDev/Boneject/wiki)

## Credits
* [Auros](https://github.com/Auros) - For creating [SiraUtil](https://github.com/Auros/SiraUtil), the inspiration for this project.
* [Danike]() - For creating BSIPA and it's Mod Init Injection system, that part of this project is based on.

## Changelog
### v1.0.0
* Complete rewrite.
* Added the capability for mods to have dependencies injected into their entrypoints at `OnMelonInitialize`, through the use of the `InjectableMelonMod` class and the `Initialize` attribute. "Mod Initialization Dependencies" can be added using the `ModInitInjector.AddInjector()` static method. 
* Re-arranged extension methods.
* Entrypoints: `App`, `Loading`, `Hub`, `Campaign`, `Player`, `Startup`, `VoidG114` all implemented.
* Better module installation system.
* Load instructions.
* Proper documentation.

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