using Boneject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches
{
    [HarmonyPatch(typeof(GameControl_Descent))]
    [HarmonyPatch(nameof(GameControl_Descent.Start))]
    internal static class GameControl_DescentPatch
    {
        [HarmonyPostfix]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once SuggestBaseTypeForParameter
        // ReSharper disable once UnusedMember.Local
        private static void Postfix(GameControl_Descent __instance)
        {
            CampaignContextLoader.Load(__instance.gameObject.GetInstanceID(), __instance);
        }
    }
}
