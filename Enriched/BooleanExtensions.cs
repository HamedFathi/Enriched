using System;

namespace Enriched.BooleanExtended
{
    public static class BooleanExtensions
    {
        public static void IfFalse(this bool @this, Action action)
        {
            if (!@this)
            {
                action();
            }
        }

        public static TResult IfFalse<TResult>(this bool value, Func<TResult> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return !value ? expression() : default;
        }

        public static TResult IfFalse<TResult>(this bool value, TResult content)
        {
            return !value ? content : default;
        }

        public static void IfTrue(this bool @this, Action action)
        {
            if (@this)
            {
                action();
            }
        }

        public static TResult IfTrue<TResult>(this bool value, Func<TResult> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            return value ? expression() : default;
        }

        public static TResult IfTrue<TResult>(this bool value, TResult content)
        {
            return value ? content : default;
        }

        public static byte ToBinary(this bool @this)
        {
            return Convert.ToByte(@this);
        }

        public static char ToSpecificChar(this bool @this, char trueValue, char falseValue)
        {
            return @this ? trueValue : falseValue;
        }

        public static string ToSpecificString(this bool @this, string trueValue, string falseValue)
        {
            return @this ? trueValue : falseValue;
        }
    }
}