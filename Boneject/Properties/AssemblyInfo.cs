using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;
using BuildInfo = Boneject.BuildInfo;

[assembly: AssemblyTitle(BuildInfo.name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(BuildInfo.company)]
[assembly: AssemblyProduct(BuildInfo.name)]
[assembly: AssemblyCopyright("""Copyright (c) 2023 Frederick ("Millzy") Mills""")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion(BuildInfo.version)]
[assembly: AssemblyFileVersion(BuildInfo.version)]
[assembly: NeutralResourcesLanguage("en")]

// ReSharper disable once RedundantArgumentDefaultValue
[assembly: MelonInfo(typeof(Mod), BuildInfo.name, BuildInfo.version, BuildInfo.author, BuildInfo.downloadLink)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonPriority(-1000000)] // -1,000,000