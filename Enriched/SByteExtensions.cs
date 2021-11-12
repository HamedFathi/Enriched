using System;

namespace Enriched.SByteExtended
{
    public static class SByteExtensions
    {
        public static sbyte Abs(this sbyte value)
        {
            return Math.Abs(value);
        }

        public static bool In(this sbyte @this, params sbyte[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static sbyte Max(this sbyte val1, sbyte val2)
        {
            return Math.Max(val1, val2);
        }

        public static sbyte Min(this sbyte val1, sbyte val2)
        {
            return Math.Min(val1, val2);
        }

        public static bool NotIn(this sbyte @this, params sbyte[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static int Sign(this sbyte value)
        {
            return Math.Sign(value);
        }
    }
}