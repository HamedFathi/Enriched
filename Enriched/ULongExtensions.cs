using System;

namespace Enriched.ULongExtended
{
    public static class ULongExtensions
    {
        public static bool Between(this ulong @this, ulong minValue, ulong maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static bool In(this ulong @this, params ulong[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this ulong @this, ulong minValue, ulong maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static ulong Max(this ulong val1, ulong val2)
        {
            return Math.Max(val1, val2);
        }

        public static ulong Min(this ulong val1, ulong val2)
        {
            return Math.Min(val1, val2);
        }

        public static bool NotIn(this ulong @this, params ulong[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}