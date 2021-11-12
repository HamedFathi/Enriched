using System;
using System.Linq;
using System.Reflection;

namespace Enriched.TypeInfoExtended
{
    public static class TypeInfoExtensions
    {
        public static bool HasInterface<TInterface>(this TypeInfo type)
                    where TInterface : class
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.HasInterface(typeof(TInterface));
        }

        public static bool HasInterface(this TypeInfo type, Type interfaceType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (interfaceType == null) throw new ArgumentNullException(nameof(interfaceType));

            return type.ImplementedInterfaces.Contains(interfaceType);
        }

        public static bool IsAssignableFrom<T>(this TypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsAssignableFrom(typeof(T).GetTypeInfo());
        }

        public static bool IsAssignableTo<T>(this TypeInfo type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsAssignableTo(typeof(T).GetTypeInfo());
        }

        public static bool IsAssignableTo(this TypeInfo type, TypeInfo other)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (other == null)
                return false;
            return other.IsAssignableFrom(type);
        }

        public static bool IsGenericTypeDefinedAs(this TypeInfo type, Type otherType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            if (!type.IsGenericType)
                return false;
            return type.GetGenericTypeDefinition() == otherType;
        }

        public static bool IsSameAsOrSubclassOf(this TypeInfo type, Type otherType)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (otherType == null) throw new ArgumentNullException(nameof(otherType));

            return type.AsType() == otherType || type.IsSubclassOf(otherType);
        }

        public static bool IsSubclassOf<T>(this TypeInfo type)
                    where T : class
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            return type.IsSubclassOf(typeof(T));
        }
    }
}