using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_MagmaGate))]
    [HarmonyPatch(nameof(GameControl_MagmaGate.Start))]
    internal class GameControl_MagmaGatePatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_MagmaGate __instance)
        {
            CampaignContextLoader.Load(__instance);
        }
    }
}
