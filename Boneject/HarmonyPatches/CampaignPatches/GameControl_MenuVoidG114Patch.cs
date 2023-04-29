using Boneject.Ninject.ContextLoaders;
using HarmonyLib;
using SLZ.Bonelab;

namespace Boneject.HarmonyPatches.CampaignPatches;

[HarmonyPatch(typeof(GameControl_MenuVoidG114))]
[HarmonyPatch(nameof(GameControl_MenuVoidG114.Start))]
public class GameControl_MenuVoidG114Patch
{
    [HarmonyPostfix]
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once SuggestBaseTypeForParameter
    // ReSharper disable once UnusedMember.Local
    private static void Postfix(GameControl_MenuVoidG114 __instance) => CampaignContextLoader.Load(__instance);
}