using Boneject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_Outro))]
    [HarmonyPatch(nameof(GameControl_Outro.Start))]
    internal class GameControl_OutroPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_Outro __instance)
        {
            CampaignContextLoader.Load(__instance.gameObject.GetInstanceID(), __instance);
        }
    }
}
