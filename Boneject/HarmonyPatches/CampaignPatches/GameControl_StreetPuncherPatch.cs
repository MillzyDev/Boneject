using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches;

[HarmonyPatch(typeof(GameControl_StreetPuncher))]
[HarmonyPatch(nameof(GameControl_StreetPuncher.Start))]
internal class GameControl_StreetPuncherPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once SuggestBaseTypeForParameter
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_StreetPuncher __instance) => CampaignContextLoader.Load(__instance);
}