using Boneject.Ninject;
using Boneject.Ninject.Modules;
using HarmonyLib;
using MelonLoader;
using Ninject;
using SLZ.Bonelab;
using SLZ.Rig;
using SLZ.SaveData;

namespace Boneject.HarmonyPatches
{
    [HarmonyPatch(typeof(GameControl_Hub))]
    [HarmonyPatch(nameof(GameControl_Hub.Start))]
    public static class GameControl_HubPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_Hub __instance)
        {
            BonejectManager bonejectManager = Mod.BonejectManager;
            BonejectKernel kernel = bonejectManager.Kernel;

            RigManager? rigManager = __instance.rm;
            Control_Player? controlPlayer = __instance.controlPlayer;
            GauntletElevator? gauntletElevator = __instance.gauntletElevator;
            InventorySaveFilter? inventorySaveFilter = __instance.inventorySaveFilter;

            var baseModule = new HubModule(__instance.gameObject.GetInstanceID(), bonejectManager, __instance, rigManager, controlPlayer,
                gauntletElevator,
                inventorySaveFilter);
            kernel.Load(baseModule);

            MelonLogger.Msg("Hub context loaded.");
        }
    }
}
