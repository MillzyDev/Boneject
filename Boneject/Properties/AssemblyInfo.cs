using System.Reflection;
using Boneject;
using MelonLoader;
using BuildInfo = Boneject.Properties.BuildInfo;

[assembly: AssemblyTitle("Boneject")]
[assembly: AssemblyDescription("A basic Ninject implementation and wrapper for BONELAB.")]
[assembly: AssemblyProduct("Boneject")]
[assembly: AssemblyCopyright("Copyright © Millzy 2023")]

[assembly: AssemblyVersion(BuildInfo.Version)]
[assembly: AssemblyFileVersion(BuildInfo.Version)]

[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(null!)]
[assembly: AssemblyTrademark(null!)]
[assembly: AssemblyCulture("")]

[assembly: MelonInfo(typeof(Mod), "Boneject", BuildInfo.Version, "Millzy")]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]