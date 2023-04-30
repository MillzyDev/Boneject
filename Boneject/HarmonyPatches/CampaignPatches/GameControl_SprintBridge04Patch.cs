using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches;

[HarmonyPatch(typeof(GameControl_SprintBridge04))]
[HarmonyPatch(nameof(GameControl_SprintBridge04.Start))]
internal class GameControl_SprintBridge04Patch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once SuggestBaseTypeForParameter
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_SprintBridge04 __instance) => CampaignContextLoader.Load(__instance);
}