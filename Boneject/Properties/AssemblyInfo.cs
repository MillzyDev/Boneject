using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;

[assembly: AssemblyTitle(nameof(Boneject))]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyProduct(nameof(Boneject))]
[assembly: AssemblyCopyright("Copyright (c) 2023 Frederick (Millzy) Mills")]
[assembly: AssemblyTrademark(null)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: AssemblyVersion("1.2.0.0")]
[assembly: AssemblyFileVersion("1.2.0")]
[assembly: NeutralResourcesLanguage("en")]

[assembly: MelonInfo(typeof(Mod), nameof(Boneject), "1.2.0", "Millzy",
    "https://github.com/MillzyDev/Boneject/releases/latest/download/Boneject.zip")]

[assembly: MelonPriority(-1000000)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]
[assembly: MelonID("dev.millzy.Boneject")]

[assembly: VerifyLoaderVersion("0.5.7")]

[assembly: HarmonyDontPatchAll]
