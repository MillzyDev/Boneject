using Boneject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_KartRace))]
    [HarmonyPatch(nameof(GameControl_KartRace.Start))]
    internal class GameControl_KartRacePatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_KartRace __instance)
        {
            CampaignContextLoader.Load(__instance.gameObject.GetInstanceID(), __instance);
        }
    }
}
