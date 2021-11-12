using System;

namespace Enriched.DecimalExtended
{
    public static class DecimalExtensions
    {
        public static decimal Abs(this decimal value)
        {
            return Math.Abs(value);
        }

        public static bool Between(this decimal @this, decimal minValue, decimal maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static decimal Ceiling(this decimal d)
        {
            return Math.Ceiling(d);
        }

        public static decimal Divide(this decimal d1, decimal d2)
        {
            return decimal.Divide(d1, d2);
        }

        public static decimal Floor(this decimal d)
        {
            return Math.Floor(d);
        }

        public static int[] GetBits(this decimal d)
        {
            return decimal.GetBits(d);
        }

        public static bool In(this decimal @this, params decimal[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this decimal @this, decimal minValue, decimal maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static decimal Max(this decimal val1, decimal val2)
        {
            return Math.Max(val1, val2);
        }

        public static decimal Min(this decimal val1, decimal val2)
        {
            return Math.Min(val1, val2);
        }

        public static decimal Multiply(this decimal d1, decimal d2)
        {
            return decimal.Multiply(d1, d2);
        }

        public static decimal Negate(this decimal d)
        {
            return decimal.Negate(d);
        }

        public static decimal Normalize(this decimal value, decimal min, decimal max)
        {
            if (max - min == 0) return 0;
            return (value - min) / (max - min);
        }

        public static bool NotIn(this decimal @this, params decimal[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this decimal number, int percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentageOf(this decimal number, decimal percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentageOf(this decimal number, long percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentOf(this decimal position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
        }

        public static decimal PercentOf(this decimal position, decimal total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
        }

        public static decimal PercentOf(this decimal position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
        }

        public static decimal Remainder(this decimal d1, decimal d2)
        {
            return decimal.Remainder(d1, d2);
        }

        public static decimal Round(this decimal d)
        {
            return Math.Round(d);
        }

        public static decimal Round(this decimal d, int decimals)
        {
            return Math.Round(d, decimals);
        }

        public static decimal Round(this decimal d, MidpointRounding mode)
        {
            return Math.Round(d, mode);
        }

        public static decimal Round(this decimal d, int decimals, MidpointRounding mode)
        {
            return Math.Round(d, decimals, mode);
        }

        public static int Sign(this decimal value)
        {
            return Math.Sign(value);
        }

        public static decimal Subtract(this decimal d1, decimal d2)
        {
            return decimal.Subtract(d1, d2);
        }

        public static byte ToByte(this decimal value)
        {
            return decimal.ToByte(value);
        }

        public static double ToDouble(this decimal d)
        {
            return decimal.ToDouble(d);
        }

        public static short ToInt16(this decimal value)
        {
            return decimal.ToInt16(value);
        }

        public static int ToInt32(this decimal d)
        {
            return decimal.ToInt32(d);
        }

        public static long ToInt64(this decimal d)
        {
            return decimal.ToInt64(d);
        }

        public static decimal ToMoney(this decimal @this)
        {
            return Math.Round(@this, 2);
        }

        public static long ToOACurrency(this decimal value)
        {
            return decimal.ToOACurrency(value);
        }

        public static sbyte ToSByte(this decimal value)
        {
            return decimal.ToSByte(value);
        }

        public static float ToSingle(this decimal d)
        {
            return decimal.ToSingle(d);
        }

        public static ushort ToUInt16(this decimal value)
        {
            return decimal.ToUInt16(value);
        }

        public static uint ToUInt32(this decimal d)
        {
            return decimal.ToUInt32(d);
        }

        public static ulong ToUInt64(this decimal d)
        {
            return decimal.ToUInt64(d);
        }

        public static decimal Truncate(this decimal d)
        {
            return Math.Truncate(d);
        }

        public static decimal Truncate(this decimal n, uint digits)
        {
            var factor = (int)Math.Pow(10, digits);
            return Math.Truncate(n * factor) / factor;
        }
    }
}