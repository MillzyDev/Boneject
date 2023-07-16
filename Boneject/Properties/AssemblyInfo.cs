using System.Reflection;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;
using BuildInfo = Boneject.BuildInfo;

[assembly: AssemblyTitle(BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct(BuildInfo.Name)]
[assembly: AssemblyCopyright($"Copyright © {BuildInfo.Author} 2023")]
[assembly: AssemblyTrademark(null!)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]

[assembly: AssemblyVersion($"{BuildInfo.Version}.*")]
[assembly: AssemblyFileVersion(BuildInfo.Version)]

[assembly: MelonInfo(
    type: typeof(Mod),
    name: BuildInfo.Name,
    version: BuildInfo.Version,
    author: BuildInfo.Author)]
    
[assembly: MelonPriority(-1000000)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID(BuildInfo.Id)]

[assembly: VerifyLoaderVersion("0.5.7")]

[assembly: HarmonyDontPatchAll]