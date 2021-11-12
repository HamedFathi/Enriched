using System;

namespace Enriched.FloatExtended
{
    public static class FloatExtensions
    {
        public static float Abs(this float value)
        {
            return Math.Abs(value);
        }

        public static bool Between(this float @this, float minValue, float maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static TimeSpan FromDays(this float days)
        {
            return TimeSpan.FromDays(days);
        }

        public static TimeSpan FromHours(this float hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan FromMilliseconds(this float milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        public static TimeSpan FromMinutes(this float minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        public static TimeSpan FromSeconds(this float seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        public static bool In(this float @this, params float[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this float @this, float minValue, float maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool IsInfinity(this float f)
        {
            return float.IsInfinity(f);
        }

        public static bool IsNaN(this float f)
        {
            return float.IsNaN(f);
        }

        public static bool IsNegativeInfinity(this float f)
        {
            return float.IsNegativeInfinity(f);
        }

        public static bool IsPositiveInfinity(this float f)
        {
            return float.IsPositiveInfinity(f);
        }

        public static float Max(this float val1, float val2)
        {
            return Math.Max(val1, val2);
        }

        public static float Min(this float val1, float val2)
        {
            return Math.Min(val1, val2);
        }

        public static bool NotIn(this float @this, params float[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this float value, int percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        public static decimal PercentageOf(this float value, float percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        public static decimal PercentageOf(this float value, double percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        public static decimal PercentageOf(this float value, long percentOf)
        {
            return (decimal)(value / percentOf * 100);
        }

        public static int Sign(this float value)
        {
            return Math.Sign(value);
        }
    }
}