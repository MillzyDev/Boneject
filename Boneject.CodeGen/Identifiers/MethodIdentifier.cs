namespace Boneject.CodeGen.Identifiers
{
    public struct MethodIdentifier
    {
        public string Name;
        public Type ReturnValue;
        public Type[] Parameters;
        public Type[] TypeParameters;

        public MethodIdentifier(string name, Type returnValue, Type[] parameters, Type[] typeParameters)
        {
            Name = name;
            ReturnValue = returnValue;
            Parameters = parameters;
            TypeParameters = typeParameters;
        }
    }
}
