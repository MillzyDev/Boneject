using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches;

[HarmonyPatch(typeof(GameControl_MoonBase))]
[HarmonyPatch(nameof(GameControl_MoonBase.Start))]
public class GameControl_MoonBasePatch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once SuggestBaseTypeForParameter
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_MoonBase __instance) => CampaignContextLoader.Load(__instance);
}