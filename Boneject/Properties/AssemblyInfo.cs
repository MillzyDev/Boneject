using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;

[assembly: AssemblyTitle(Boneject.BuildInfo.name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyProduct(Boneject.BuildInfo.name)]
[assembly: AssemblyCopyright("Copyright (c) 2023 Frederick (Millzy) Mills")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion(Boneject.BuildInfo.version)]
[assembly: AssemblyFileVersion(Boneject.BuildInfo.semVersion)]
[assembly: NeutralResourcesLanguage("en")]

[assembly: MelonInfo(typeof(Mod), Boneject.BuildInfo.name, Boneject.BuildInfo.semVersion, Boneject.BuildInfo.author, 
    Boneject.BuildInfo.downloadLink)]

[assembly: MelonPriority(-1000000)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID("{5}")]

[assembly: VerifyLoaderVersion("{6}")]

[assembly: HarmonyDontPatchAll]