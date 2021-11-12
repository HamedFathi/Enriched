using System;
using System.Collections.Generic;
using System.Linq;

namespace Enriched.RandomExtended
{
    public static class RandomExtensions
    {
        public static bool CoinToss(this Random @this)
        {
            return @this.Next(2) == 0;
        }

        public static decimal NextDecimal(this Random @this)
        {
            var sign = @this.Next(2) == 1;
            return @this.NextDecimal(sign);
        }

        public static decimal NextDecimal(this Random @this, bool sign)
        {
            var scale = (byte)@this.Next(29);
            return new decimal(@this.NextInt32(),
                @this.NextInt32(),
                @this.NextInt32(),
                sign,
                scale);
        }

        public static decimal NextDecimal(this Random @this, decimal maxValue)
        {
            return @this.NextNonNegativeDecimal() / decimal.MaxValue * maxValue;
            ;
        }

        public static decimal NextDecimal(this Random @this, decimal minValue, decimal maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new InvalidOperationException();
            }
            var range = maxValue - minValue;
            return @this.NextDecimal(range) + minValue;
        }

        public static double NextDouble(this Random @this, double min, double max)
        {
            return @this.NextDouble() * (max - min) + min;
        }

        public static T NextEnum<T>(this System.Random random) where T : Enum
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        public static int NextInt32(this Random @this)
        {
            var firstBits = @this.Next(0, 1 << 4) << 28;
            var lastBits = @this.Next(0, 1 << 28);
            return firstBits | lastBits;
        }

        public static long NextInt64(this Random @this, long maxValue)
        {
            return (long)(@this.NextNonNegativeLong() / (double)long.MaxValue * maxValue);
        }

        public static long NextInt64(this Random @this, long minValue, long maxValue)
        {
            if (minValue >= maxValue)
            {
                throw new InvalidOperationException();
            }
            var range = maxValue - minValue;
            return @this.NextInt64(range) + minValue;
        }

        public static long NextInt64(this Random @this)
        {
            var buffer = new byte[sizeof(long)];
            @this.NextBytes(buffer);
            return BitConverter.ToInt64(buffer, 0);
        }

        public static decimal NextNonNegativeDecimal(this Random @this)
        {
            return @this.NextDecimal(false);
        }

        public static long NextNonNegativeLong(this Random @this)
        {
            var bytes = new byte[sizeof(long)];
            @this.NextBytes(bytes);
            // strip out the sign bit
            bytes[7] = (byte)(bytes[7] & 0x7f);
            return BitConverter.ToInt64(bytes, 0);
        }

        public static T PickOneOf<T>(this Random @this, params T[] values)
        {
            return values[@this.Next(values.Length)];
        }

        public static T PickOneOf<T>(this Random @this, IEnumerable<T> values)
        {
            var arr = values.ToArray();
            return @this.PickOneOf(arr);
        }
    }
}