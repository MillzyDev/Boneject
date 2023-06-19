using System.Reflection;
using Boneject.MelonLoader;
using Boneject.Ninject;
using MelonLoader;
using UnityEngine;

namespace Boneject
{
    // not much point in using mod init injection before it even does anything useful
    internal class Mod : MelonMod
    {
        private HarmonyLib.Harmony _harmony = null!;
        private BonejectManager _bonejectManager = null!;

        public Mod()
        {
            Instance = this;
        }

        private static Mod Instance { get; set; } = null!;

        // need access to the boneject manager in harmony patches, which need to be static.
        internal static BonejectManager BonejectManager
        {
            get => Instance._bonejectManager;
        }

        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once ConvertToAutoPropertyWithPrivateSetter
        public new HarmonyLib.Harmony HarmonyInstance
        {
            get => _harmony; // Hide default harmony field.
        }

        public override void OnInitializeMelon()
        {
            _harmony = new HarmonyLib.Harmony("dev.millzy.Boneject");
            _bonejectManager = new BonejectManager();

            ModInitInjector.AddInjector(typeof(Bonejector), ConstructBonejector);
        }

        public override void OnLateInitializeMelon()
        {
            _harmony.PatchAll();
            _bonejectManager.Enable();

            GameObject gameObject = new("Boneject Context Handler");
            gameObject.SetActive(false);
            var contextHandler = gameObject.AddComponent<BonejectContextHandler>();
            contextHandler.BonejectManager = _bonejectManager;
            gameObject.SetActive(true);

            _ = _bonejectManager.Kernel; // Force the kernel to init.
        }

        public override void OnDeinitializeMelon()
        {
            _bonejectManager.Disable();
            _harmony.UnpatchSelf();
        }

        private object ConstructBonejector(object? previous, ParameterInfo? _, MelonInfoAttribute info)
        {
            if (previous is not null)
                return previous;

            Bonejector bonejector = new(info);
            _bonejectManager.Add(bonejector);
            return bonejector;
        }
    }
}
