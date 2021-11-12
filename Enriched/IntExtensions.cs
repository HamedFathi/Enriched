using Enriched.EnumExtended;
using System;
using System.Data;
using System.Net;
using System.Numerics;

namespace Enriched.IntExtended
{
    public static class IntExtensions
    {
        public static int Abs(this int value)
        {
            return Math.Abs(value);
        }

        public static bool Between(this int @this, int minValue, int maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static long BigMul(this int a, int b)
        {
            return Math.BigMul(a, b);
        }

        public static BigInteger Combination(this int n, int r)
        {
            return n.Factorial() / ((n - r).Factorial() * r.Factorial());
        }

        public static string ConvertFromUtf32(this int utf32)
        {
            return char.ConvertFromUtf32(utf32);
        }

        public static int DaysInMonth(this int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        public static int DivRem(this int a, int b, out int result)
        {
            return Math.DivRem(a, b, out result);
        }

        public static BigInteger Factorial(this int n)
        {
            BigInteger product = 1;
            for (int i = 1; i <= n; i++)
            {
                product *= i;
            }
            return product;
        }

        public static bool FactorOf(this int @this, int factorNumber)
        {
            return factorNumber % @this == 0;
        }

        public static TimeSpan FromDays(this int days)
        {
            return TimeSpan.FromDays(days);
        }

        public static TimeSpan FromHours(this int hours)
        {
            return TimeSpan.FromHours(hours);
        }

        public static TimeSpan FromMilliseconds(this int milliseconds)
        {
            return TimeSpan.FromMilliseconds(milliseconds);
        }

        public static TimeSpan FromSeconds(this int seconds)
        {
            return TimeSpan.FromSeconds(seconds);
        }

        public static TimeSpan FromTicks(this int ticks)
        {
            return TimeSpan.FromTicks(ticks);
        }

        public static int GetArrayIndex(this int at)
        {
            return at == 0 ? 0 : at - 1;
        }

        public static byte[] GetBytes(this int value)
        {
            return BitConverter.GetBytes(value);
        }

        public static string GetEnumDescription(this int @this, Type enumType, bool replaceNullWithEnumName = false)
        {
            if (!enumType.IsEnum) return null;
            var enumValue = Enum.ToObject(enumType, @this);
            return ((Enum)enumValue).GetDescription(replaceNullWithEnumName);
        }

        public static int HostToNetworkOrder(this int host)
        {
            return IPAddress.HostToNetworkOrder(host);
        }

        public static TimeSpan Hours(this int @this)
        {
            return TimeSpan.FromHours(@this);
        }

        public static bool In(this int @this, params int[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this int @this, int minValue, int maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool IsEven(this int @this)
        {
            return @this % 2 == 0;
        }

        public static bool IsIndexInArray(this int index, Array arrayToCheck)
        {
            return index.GetArrayIndex().InRange(arrayToCheck.GetLowerBound(0), arrayToCheck.GetUpperBound(0));
        }

        public static bool IsLeapYear(this int year)
        {
            return DateTime.IsLeapYear(year);
        }

        public static bool IsMultipleOf(this int @this, int factor)
        {
            return @this % factor == 0;
        }

        public static bool IsOdd(this int @this)
        {
            return @this % 2 != 0;
        }

        public static bool IsPerfect(this int num)
        {
            int sum = 0;
            if (num == 0 || num == 1)
                return false;
            else
            {
                for (int i = 2; i <= num / 2; i++)
                {
                    if (num % i == 0)
                    {
                        sum += i;
                    }
                }
                if (sum + 1 == num)
                {
                    return true;
                }
                else
                    return false;
            }
        }

        public static bool IsPrime(this int a)
        {
            int temp = a;
            if (a == 0 || a == 1)
                return false;
            if (a < 0)
            {
                temp = a * (-1);
            }
            for (int j = 2; j <= temp / 2; j++)
            {
                if (a % j == 0)
                    return false;
            }
            return true;
        }

        public static int Max(this int val1, int val2)
        {
            return Math.Max(val1, val2);
        }

        public static TimeSpan Milliseconds(this int @this)
        {
            return TimeSpan.FromMilliseconds(@this);
        }

        public static int Min(this int val1, int val2)
        {
            return Math.Min(val1, val2);
        }

        public static TimeSpan Minutes(this int @this)
        {
            return TimeSpan.FromMinutes(@this);
        }

        public static int NetworkToHostOrder(this int network)
        {
            return IPAddress.NetworkToHostOrder(network);
        }

        public static bool NotIn(this int @this, params int[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static decimal PercentageOf(this int number, int percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentageOf(this int number, float percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this int number, double percent)
        {
            return (decimal)(number * percent / 100);
        }

        public static decimal PercentageOf(this int number, decimal percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentageOf(this int number, long percent)
        {
            return number * percent / 100;
        }

        public static decimal PercentOf(this int position, int total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
        }

        public static decimal PercentOf(this int? position, int total)
        {
            if (position == null) return 0;

            decimal result = 0;
            if (position > 0 && total > 0)
                result = (decimal)position / total * 100;
            return result;
        }

        public static decimal PercentOf(this int position, float total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
        }

        public static decimal PercentOf(this int position, double total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
        }

        public static decimal PercentOf(this int position, decimal total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / total * 100;
            return result;
        }

        public static decimal PercentOf(this int position, long total)
        {
            decimal result = 0;
            if (position > 0 && total > 0)
                result = position / (decimal)total * 100;
            return result;
        }

        public static BigInteger Permutation(this int n, int r)
        {
            return n.Factorial() / (n - r).Factorial();
        }

        public static TimeSpan Seconds(this int @this)
        {
            return TimeSpan.FromSeconds(@this);
        }

        public static int Sign(this int value)
        {
            return Math.Sign(value);
        }

        public static SqlDbType SqlSystemTypeToSqlDbType(this int @this)
        {
            switch (@this)
            {
                case 34: // 34 | "image" | SqlDbType.Image
                    return SqlDbType.Image;

                case 35: // 35 | "text" | SqlDbType.Text
                    return SqlDbType.Text;

                case 36: // 36 | "uniqueidentifier" | SqlDbType.UniqueIdentifier
                    return SqlDbType.UniqueIdentifier;

                case 40: // 40 | "date" | SqlDbType.Date
                    return SqlDbType.Date;

                case 41: // 41 | "time" | SqlDbType.Time
                    return SqlDbType.Time;

                case 42: // 42 | "datetime2" | SqlDbType.DateTime2
                    return SqlDbType.DateTime2;

                case 43: // 43 | "datetimeoffset" | SqlDbType.DateTimeOffset
                    return SqlDbType.DateTimeOffset;

                case 48: // 48 | "tinyint" | SqlDbType.TinyInt
                    return SqlDbType.TinyInt;

                case 52: // 52 | "smallint" | SqlDbType.SmallInt
                    return SqlDbType.SmallInt;

                case 56: // 56 | "int" | SqlDbType.Int
                    return SqlDbType.Int;

                case 58: // 58 | "smalldatetime" | SqlDbType.SmallDateTime
                    return SqlDbType.SmallDateTime;

                case 59: // 59 | "real" | SqlDbType.Real
                    return SqlDbType.Real;

                case 60: // 60 | "money" | SqlDbType.Money
                    return SqlDbType.Money;

                case 61: // 61 | "datetime" | SqlDbType.DateTime
                    return SqlDbType.DateTime;

                case 62: // 62 | "float" | SqlDbType.Float
                    return SqlDbType.Float;

                case 98: // 98 | "sql_variant" | SqlDbType.Variant
                    return SqlDbType.Variant;

                case 99: // 99 | "ntext" | SqlDbType.NText
                    return SqlDbType.NText;

                case 104: // 104 | "bit" | SqlDbType.Bit
                    return SqlDbType.Bit;

                case 106: // 106 | "decimal" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 108: // 108 | "numeric" | SqlDbType.Decimal
                    return SqlDbType.Decimal;

                case 122: // 122 | "smallmoney" | SqlDbType.SmallMoney
                    return SqlDbType.SmallMoney;

                case 127: // 127 | "bigint" | SqlDbType.BigInt
                    return SqlDbType.BigInt;

                case 165: // 165 | "varbinary" | SqlDbType.VarBinary
                    return SqlDbType.VarBinary;

                case 167: // 167 | "varchar" | SqlDbType.VarChar
                    return SqlDbType.VarChar;

                case 173: // 173 | "binary" | SqlDbType.Binary
                    return SqlDbType.Binary;

                case 175: // 175 | "char" | SqlDbType.Char
                    return SqlDbType.Char;

                case 189: // 189 | "timestamp" | SqlDbType.Timestamp
                    return SqlDbType.Timestamp;

                case 231: // 231 | "nvarchar", "sysname" | SqlDbType.NVarChar
                    return SqlDbType.NVarChar;

                case 239: // 239 | "nchar" | SqlDbType.NChar
                    return SqlDbType.NChar;

                case 240: // 240 | "hierarchyid", "geometry", "geography" | SqlDbType.Udt
                    return SqlDbType.Udt;

                case 241: // 241 | "xml" | SqlDbType.Xml
                    return SqlDbType.Xml;

                default:
                    throw new Exception(
                        $"Unsupported Type: {@this}. Please let us know about this type and we will support it: sales@zzzprojects.com");
            }
        }

        public static int SumOfDigets(this int a)
        {
            int sum = 0;
            while (a / 10 != 0)
            {
                sum += (a % 10);
                a /= 10;
            }
            return sum + a;
        }

        public static void Times(this int value, Action action)
        {
            for (var i = 0; i < value; i++)
                action();
        }

        public static void Times(this int value, Action<int> action)
        {
            for (var i = 0; i < value; i++)
                action(i);
        }

        public static T TimesOrUntil<T>(this int times, Func<T> retrieve, Func<T, bool> validate, Action inBetween = null)
        {
            T state;
            var count = 0;

            while (true)
            {
                state = retrieve();

                if (validate(state) || ++count >= times)
                    break;

                if (inBetween != null) inBetween();
            }

            return state;
        }

        public static TimeSpan Weeks(this int @this)
        {
            return TimeSpan.FromDays(@this * 7);
        }
    }
}