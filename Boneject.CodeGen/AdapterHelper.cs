using System.Collections;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace Boneject.CodeGen
{
    public sealed class AdapterHelper
    {
        private readonly TypeDefinition _masterType;
        private readonly TypeDefinition[] _typeDefinitions;
        
        public AdapterHelper(TypeDefinition masterType, params TypeDefinition[] typeDefinitions)
        {
            _masterType = masterType;
            _typeDefinitions = typeDefinitions;
        }

        public IEnumerable<FieldDefinition> GetCommonFields()
        {
            Collection<FieldDefinition> fields = _masterType.Fields;

            IEnumerable<FieldDefinition> commonFields = fields.Where(field =>
            {
                return _typeDefinitions
                    .Select(typeDefinition => typeDefinition.Fields
                        .Any(otherField =>
                            field.Name == otherField.Name &&
                            field.DeclaringType.FullName == otherField.DeclaringType.FullName &&
                            field.IsStatic == otherField.IsStatic
                        )
                    ).All(match => match);
            });
            
            return commonFields;
        }
    }
}
