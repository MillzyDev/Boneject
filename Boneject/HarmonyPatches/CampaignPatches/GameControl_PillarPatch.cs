using Boneject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_Pillar))]
    [HarmonyPatch(nameof(GameControl_Pillar.Start))]
    internal class GameControl_PillarPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_Pillar __instance)
        {
            CampaignContextLoader.Load(__instance.gameObject.GetInstanceID(), __instance);
        }
    }
}
