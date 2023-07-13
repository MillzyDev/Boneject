namespace Boneject.CodeGen.Identifiers
{
    public struct FieldIdentifier
    {
        public string Name;
        public Type Type;

        public FieldIdentifier(string name, Type type)
        {
            Name = name;
            Type = type;
        }
    }
}
