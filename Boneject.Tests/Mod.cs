using Boneject.MelonLoader;
using Boneject.MelonLoader.Attributes;
using Boneject.Ninject;

namespace Boneject.Tests
{
    public class Mod : InjectableMelonMod
    {
        [OnInitialize]
        public void OnInitializeMod(Bonejector bonejector)
        {
            LoggerInstance.Msg("Starting Boneject tests.");
            LoggerInstance.Msg($"Injected Bonejector instance is {NullString.Create(bonejector)}");
        }
    }
}