using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_Ascent))]
    [HarmonyPatch(nameof(GameControl_Ascent.Start))]
    internal static class GameControl_AscentPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_Ascent __instance)
        {
            CampaignContextLoader.Load(__instance);
        }
    }
}
