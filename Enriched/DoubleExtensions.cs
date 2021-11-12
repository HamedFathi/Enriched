using System;

namespace Enriched.DoubleExtended
{
    public static class DoubleExtensions
    {
        public static double Abs(this double value)
        {
            return Math.Abs(value);
        }

        public static double Acos(this double d)
        {
            return Math.Acos(d);
        }

        public static double Asin(this double d)
        {
            return Math.Asin(d);
        }

        public static double Atan(this double d)
        {
            return Math.Atan(d);
        }

        public static double Atan2(this double y, double x)
        {
            return Math.Atan2(y, x);
        }

        public static bool Between(this double @this, double minValue, double maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static double Ceiling(this double a)
        {
            return Math.Ceiling(a);
        }

        public static double Cos(this double d)
        {
            return Math.Cos(d);
        }

        public static double Cosh(this double value)
        {
            return Math.Cosh(value);
        }

        public static double Exp(this double d)
        {
            return Math.Exp(d);
        }

        public static double Floor(this double d)
        {
            return Math.Floor(d);
        }

        public static TimeSpan FromDays(this double value)
        {
            return TimeSpan.FromDays(value);
        }

        public static TimeSpan FromHours(this double value)
        {
            return TimeSpan.FromHours(value);
        }

        public static TimeSpan FromMilliseconds(this double value)
        {
            return TimeSpan.FromMilliseconds(value);
        }

        public static TimeSpan FromMinutes(this double value)
        {
            return TimeSpan.FromMinutes(value);
        }

        public static DateTime FromOADate(this double d)
        {
            return DateTime.FromOADate(d);
        }

        public static TimeSpan FromSeconds(this double value)
        {
            return TimeSpan.FromSeconds(value);
        }

        public static double IEEERemainder(this double x, double y)
        {
            return Math.IEEERemainder(x, y);
        }

        public static bool In(this double @this, params double[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this double @this, double minValue, double maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool IsInfinity(this double d)
        {
            return double.IsInfinity(d);
        }

        public static bool IsNaN(this double d)
        {
            return double.IsNaN(d);
        }

        public static bool IsNegativeInfinity(this double d)
        {
            return double.IsNegativeInfinity(d);
        }

        public static bool IsPositiveInfinity(this double d)
        {
            return double.IsPositiveInfinity(d);
        }

        public static double Log(this double d)
        {
            return Math.Log(d);
        }

        public static double Log(this double d, double newBase)
        {
            return Math.Log(d, newBase);
        }

        public static double Log10(this double d)
        {
            return Math.Log10(d);
        }

        public static double Max(this double val1, double val2)
        {
            return Math.Max(val1, val2);
        }

        public static double Min(this double val1, double val2)
        {
            return Math.Min(val1, val2);
        }

        public static double Normalize(this double value, double min, double max)
        {
            if (max - min == 0) return 0;
            return (value - min) / (max - min);
        }

        public static bool NotIn(this double @this, params double[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this double number, int percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this double number, long percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentOf(this double position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static decimal PercentOf(this double position, float total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static decimal PercentOf(this double position, double total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static decimal PercentOf(this double position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)((decimal)position / (decimal)total * 100);
            return result;
        }

        public static double Pow(this double x, double y)
        {
            return Math.Pow(x, y);
        }

        public static double Round(this double a)
        {
            return Math.Round(a);
        }

        public static double Round(this double a, int digits)
        {
            return Math.Round(a, digits);
        }

        public static double Round(this double a, MidpointRounding mode)
        {
            return Math.Round(a, mode);
        }

        public static double Round(this double a, int digits, MidpointRounding mode)
        {
            return Math.Round(a, digits, mode);
        }

        public static double Sigmoid(this double value)
        {
            return 1.0 / (1.0 + Math.Exp(-value));
        }

        public static double SigmoidDerivative(this double value)
        {
            return Sigmoid(value) * (1 - Sigmoid(value));
        }

        public static int Sign(this double value)
        {
            return Math.Sign(value);
        }

        public static double Sin(this double a)
        {
            return Math.Sin(a);
        }

        public static double Sinh(this double value)
        {
            return Math.Sinh(value);
        }

        public static double Sqrt(this double d)
        {
            return Math.Sqrt(d);
        }

        public static double Tan(this double a)
        {
            return Math.Tan(a);
        }

        public static double Tanh(this double value)
        {
            return Math.Tanh(value);
        }

        public static double ToMoney(this double @this)
        {
            return Math.Round(@this, 2);
        }

        public static double Truncate(this double d)
        {
            return Math.Truncate(d);
        }
    }
}