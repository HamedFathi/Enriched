using System;
using System.Net;

namespace Enriched.LongExtended
{
    public static class LongExtensions
    {
        public static long Abs(this long value)
        {
            return Math.Abs(value);
        }

        public static bool Between(this long @this, long minValue, long maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static DateTime ConvertFromUnixTimeStamp(this long timestamp)
        {
            var dt = new DateTime(1970, 1, 1);
            return dt.AddSeconds(timestamp);
        }

        public static TimeSpan Days(this long @this)
        {
            return TimeSpan.FromDays(@this);
        }

        public static long DivRem(this long a, long b, out long result)
        {
            return Math.DivRem(a, b, out result);
        }

        public static bool FactorOf(this long @this, long factorNumer)
        {
            return factorNumer % @this == 0;
        }

        public static DateTime FromBinary(this long dateData)
        {
            return DateTime.FromBinary(dateData);
        }

        public static TimeSpan FromDays(this long days)
        {
            return TimeSpan.FromDays(days);
        }

        public static DateTime FromFileTime(this long fileTime)
        {
            return DateTime.FromFileTime(fileTime);
        }

        public static DateTime FromFileTimeUtc(this long fileTime)
        {
            return DateTime.FromFileTimeUtc(fileTime);
        }

        public static TimeSpan FromHours(this long hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan FromMilliseconds(this long milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        public static TimeSpan FromMinutes(this long minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        public static decimal FromOACurrency(this long cy)
        {
            return decimal.FromOACurrency(cy);
        }

        public static TimeSpan FromSeconds(this long seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        public static TimeSpan FromTicks(this long value)
        {
            return TimeSpan.FromTicks(value);
        }

        public static byte[] GetBytes(this long value)
        {
            return BitConverter.GetBytes(value);
        }

        public static long HostToNetworkOrder(this long host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        public static TimeSpan Hours(this long @this)
        {
            return TimeSpan.FromHours(@this);
        }

        public static bool In(this long @this, params long[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this long @this, long minValue, long maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static double Int64BitsToDouble(this long value)
        {
            return BitConverter.Int64BitsToDouble(value);
        }

        public static bool IsEven(this long @this)
        {
            return @this % 2 == 0;
        }

        public static bool IsMultipleOf(this long @this, long factor)
        {
            return @this % factor == 0;
        }

        public static bool IsOdd(this long @this)
        {
            return @this % 2 != 0;
        }

        public static bool IsPrime(this long @this)
        {
            if (@this == 1 || @this == 2)
            {
                return true;
            }

            if (@this % 2 == 0)
            {
                return false;
            }

            var sqrt = (long)Math.Sqrt(@this);
            for (long t = 3; t <= sqrt; t = t + 2)
            {
                if (@this % t == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static long Max(this long val1, long val2)
        {
            return Math.Max(val1, val2);
        }

        public static TimeSpan Milliseconds(this long @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }

        public static long Min(this long val1, long val2)
        {
            return Math.Min(val1, val2);
        }

        public static TimeSpan Minutes(this long @this)
        {
            return TimeSpan.FromMinutes(@this);
        }

        public static long NetworkToHostOrder(this long network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }

        public static bool NotIn(this long @this, params long[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this long number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this long number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this long number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this long number, decimal percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this long number, long percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentOf(this long position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }

        public static decimal PercentOf(this long position, float total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static decimal PercentOf(this long position, double total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static decimal PercentOf(this long position, decimal total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }

        public static decimal PercentOf(this long position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / (decimal)total * 100;
            return result;
        }

        public static TimeSpan Seconds(this long @this)
        {
            return TimeSpan.FromSeconds(@this);
        }

        public static int Sign(this long value)
        {
            return Math.Sign(value);
        }

        public static void Times(this long value, Action action)
        {
            for (var i = 0; i < value; i++)
                action();
        }

        public static void Times(this long value, Action<long> action)
        {
            for (var i = 0; i < value; i++)
                action(i);
        }

        public static string ToBase(this long input, string baseChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz")
        {
            var text = string.Empty;
            var targetBase = baseChars.Length;
            do
            {
                text = $"{baseChars[(int)(input % targetBase)]}{text}";
                input /= targetBase;
            } while (input > 0);

            return text;
        }

        public static TimeSpan Weeks(this long @this)
        {
            return TimeSpan.FromDays(@this * 7);
        }
    }
}