using System.Reflection;
using System.Runtime.InteropServices;
using Boneject;
using MelonLoader;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(Boneject.BuildInfo.Name)]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(Boneject.BuildInfo.Company)]
[assembly: AssemblyProduct(Boneject.BuildInfo.Name)]
[assembly: AssemblyCopyright("Copyright © " + Boneject.BuildInfo.Author + " 2023")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

[assembly: ComVisible(false)]
[assembly: Guid("D4558814-9822-4089-8600-5A07FBBCD2A3")]

[assembly: AssemblyVersion(Boneject.BuildInfo.Version)]
[assembly: AssemblyFileVersion(Boneject.BuildInfo.Version)]

[assembly: MelonInfo(typeof(Mod), Boneject.BuildInfo.Name, Boneject.BuildInfo.Version, Boneject.BuildInfo.Author)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]