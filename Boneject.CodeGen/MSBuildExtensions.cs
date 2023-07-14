using Microsoft.CodeAnalysis;

namespace Boneject.CodeGen
{
    public static class MSBuildExtensions
    {
        public static string GetMSBuildProperty(this GeneratorExecutionContext context, string name, 
                                                string defaultValue = "")
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"build_property.{name}", out string? value);
            return value ?? defaultValue;
        }
    }
}
