using System.Reflection;

namespace Boneject.CodeGen
{
    public struct AdapterTarget
    {
        public readonly Assembly Assembly;
        public readonly string Class;

        public AdapterTarget(Assembly assembly, string @class)
        {
            Assembly = assembly;
            Class = @class;
        }
    }
}
