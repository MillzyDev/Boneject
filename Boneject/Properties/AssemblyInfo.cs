using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;
using BuildInfo = Boneject.BuildInfo;

[assembly: AssemblyTitle(BuildInfo.name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyProduct(BuildInfo.name)]
[assembly: AssemblyCopyright("Copyright (c) 2023 Frederick (Millzy) Mills")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion(BuildInfo.version)]
[assembly: AssemblyFileVersion(BuildInfo.semVersion)]
[assembly: NeutralResourcesLanguage("en")]

[assembly: MelonInfo(typeof(Mod), BuildInfo.name, BuildInfo.semVersion, BuildInfo.author, 
    BuildInfo.downloadLink)]

[assembly: MelonPriority(-1000000)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID("{5}")]

[assembly: VerifyLoaderVersion(BuildInfo.melonLoaderVersion)]

[assembly: HarmonyDontPatchAll]