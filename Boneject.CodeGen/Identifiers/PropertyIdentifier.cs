namespace Boneject.CodeGen.Identifiers
{
    public struct PropertyIdentifier
    {
        public string Name;
        public Type Type;
        public bool HasGetter;
        public bool HasSetter;

        public PropertyIdentifier(string name, Type type, bool hasGetter, bool hasSetter)
        {
            Name = name;
            Type = type;
            HasGetter = hasGetter;
            HasSetter = hasSetter;
        }
    }
}
