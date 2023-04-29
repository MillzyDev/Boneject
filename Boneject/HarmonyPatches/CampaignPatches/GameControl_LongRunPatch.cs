using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches;

[HarmonyPatch(typeof(GameControl_LongRun))]
[HarmonyPatch(nameof(GameControl_LongRun.Start))]
public class GameControl_LongRunPatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once SuggestBaseTypeForParameter
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_LongRun __instance) => CampaignContextLoader.Load(__instance);
}