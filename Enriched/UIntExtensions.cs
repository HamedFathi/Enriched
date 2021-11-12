using System;

namespace Enriched.UIntExtended
{
    public static class UIntExtensions
    {
        public static bool Between(this uint @this, uint minValue, uint maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static bool In(this uint @this, params uint[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this uint @this, uint minValue, uint maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static uint Max(this uint val1, uint val2)
        {
            return Math.Max(val1, val2);
        }

        public static uint Min(this uint val1, uint val2)
        {
            return Math.Min(val1, val2);
        }

        public static bool NotIn(this uint @this, params uint[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }
    }
}