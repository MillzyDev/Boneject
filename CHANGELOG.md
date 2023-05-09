# v1.0.1
* Gets bindings to register using an expression based method rather than a reflection based one. (should be faster)
* Bindings are only bound to the active scene when loaded. (unless loading screens mess this up)
* Ninject now fully handles mod init injection. (again, speed)

# v1.0.0
* Complete rewrite.
* Added the capability for mods to have dependencies injected into their entrypoints at `OnMelonInitialize`, through the use of the `InjectableMelonMod` class and the `Initialize` attribute. "Mod Initialization Dependencies" can be added using the `ModInitInjector.AddInjector()` static method.
* Re-arranged extension methods.
* Entrypoints: `App`, `Loading`, `Hub`, `Campaign`, `Player`, `Startup`, `VoidG114` all implemented.
* Better module installation system.
* Load instructions.
* Proper documentation.

# v0.3.0
* Removed `AsComponentOnNewGameObject<T>` and `AsComponentOnExistingGameObject<T>` extension methods due to Ninject not injecting into constant bindings and not being able to access the Kernel to fix it.
* Added `BindComponentOnNewGameObject<T>` and `BindComponentOnExistingGameObject<T>` extension methods to replace the aforementioned.

# v0.2.0
* Removed `App` location and its associated ModuleLoader and HarmonyPatch. This is due to the behaviour being different than desired. GlobalDependencies must now be added manually using the `GlobalDependencies` static class. Ninject can still handle their creation however.
* Added a `Game` location, that should load in every level, except the two main menu levels. An instance of `BonelabGameControl` is automatically loaded into the Ninject Kernel before the modules are loaded.
* Added a `Loading` location, its the loading screen basically. An instance of `LoadingScene` is automatically loaded into the Ninject Kernel before modules are loaded.
* Added more logging messages.
* Made it so that instances of the Ninject Kernel are limited to one per scene (in theory). Should help with compatibility with some future things.
* Fixed a bug (logic error) where the Ninject Kernel would only be initialized when modules are loaded, preventing "automatically bound dependencies" from actually being bound to the Ninject Kernel.
* Fixed a bug (another logic error) where global dependencies that are created by Ninject would not be updated in the global dependency dictionary. This may have been the problem causing the first issue, I can add the App location back if requested.

# v0.1.0
* Initial Release.