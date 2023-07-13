using System;
using System.Reflection;
using MelonLoader;

namespace Boneject
{
    internal readonly struct TypedInjector : IEquatable<TypedInjector>
    {
        public readonly Type Type;
        private readonly InjectParameter _injector;

        public TypedInjector(Type type, InjectParameter injector)
        {
            Type = type;
            _injector = injector;
        }

        public object? Inject(object? previous, ParameterInfo parameter, MelonInfoAttribute info)
        {
            return _injector(previous, parameter, info);
        }

        public bool Equals(TypedInjector other)
        {
            return Type == other.Type && _injector == other._injector;
        }

        public override bool Equals(object obj)
        {
            return obj is TypedInjector other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode() ^ _injector.GetHashCode();
        }

        public static bool operator ==(TypedInjector left, TypedInjector right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TypedInjector left, TypedInjector right)
        {
            return !left.Equals(right);
        }
    }
}
