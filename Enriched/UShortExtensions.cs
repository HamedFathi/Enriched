using System;

namespace Enriched.UShortExtended
{
    public static class UShortExtensions
    {
        public static bool Between(this ushort @this, ushort minValue, ushort maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static bool In(this ushort @this, params ushort[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this ushort @this, ushort minValue, ushort maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static ushort Max(this ushort val1, ushort val2)
        {
            return Math.Max(val1, val2);
        }

        public static ushort Min(this ushort val1, ushort val2)
        {
            return Math.Min(val1, val2);
        }

        public static bool NotIn(this ushort @this, params ushort[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}