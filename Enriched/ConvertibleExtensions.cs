using System;

namespace Enriched.ConvertibleExtended
{
    public static class ConvertibleExtensions
    {
        public static T ConvertTo<T>(this IConvertible obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        public static T ConvertToOrDefault<T>(this IConvertible obj)
        {
            try
            {
                return ConvertTo<T>(obj);
            }
            catch
            {
                return default;
            }
        }

        public static bool ConvertToOrDefault<T>(this IConvertible obj, out T newObj)
        {
            try
            {
                newObj = ConvertTo<T>(obj);
                return true;
            }
            catch
            {
                newObj = default;
                return false;
            }
        }

        public static T ConvertToOrNull<T>(this IConvertible obj)
                    where T : class
        {
            try
            {
                return ConvertTo<T>(obj);
            }
            catch
            {
                return null;
            }
        }

        public static bool ConvertToOrNull<T>(this IConvertible obj, out T newObj)
                    where T : class
        {
            try
            {
                newObj = ConvertTo<T>(obj);
                return true;
            }
            catch
            {
                newObj = null;
                return false;
            }
        }

        public static T ConvertToOrOther<T>(this IConvertible obj, T other)
        {
            try
            {
                return ConvertTo<T>(obj);
            }
            catch
            {
                return other;
            }
        }

        public static bool ConvertToOrOther<T>(this IConvertible obj, out T newObj, T other)
        {
            try
            {
                newObj = ConvertTo<T>(obj);
                return true;
            }
            catch
            {
                newObj = other;
                return false;
            }
        }
    }
}