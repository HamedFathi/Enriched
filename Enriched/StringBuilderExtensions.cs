using System;
using System.Collections.Generic;
using System.Text;

namespace Enriched.StringBuilderExtended
{
    public static class StringBuilderExtensions
    {
        public static void Append(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.Append(string.Format(value, parameters));
        }

        public static StringBuilder AppendFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.Append(string.Format(format, args));

            return @this;
        }

        public static StringBuilder AppendFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.Append(string.Format(format, args));

            return @this;
        }

        public static StringBuilder AppendFormatIf(this StringBuilder sb, bool condition, string format,
                                          params object[] args)
        {
            if (condition) sb.AppendFormat(format, args);
            return sb;
        }

        public static void AppendIf(this StringBuilder sb, bool condition, char text)
        {
            if (condition)
            {
                sb.Append(text);
            }
        }

        public static void AppendIf(this StringBuilder sb, bool condition, string text)
        {
            if (condition)
            {
                sb.Append(text);
            }
        }

        public static StringBuilder AppendIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.Append(value);
                }
            }
            return @this;
        }

        public static StringBuilder AppendIf(this StringBuilder sb, bool condition, object value)
        {
            if (condition) sb.Append(value);
            return sb;
        }

        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.Append(string.Join(separator, values));
            return @this;
        }

        public static StringBuilder AppendJoin<T>(this StringBuilder @this, string separator, params T[] values)
        {
            @this.Append(string.Join(separator, values));
            return @this;
        }

        public static void AppendLine(this StringBuilder builder, string value, params object[] parameters)
        {
            builder.AppendLine(string.Format(value, parameters));
        }

        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, params object[] args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        public static StringBuilder AppendLineFormat(this StringBuilder @this, string format, List<IEnumerable<object>> args)
        {
            @this.AppendLine(string.Format(format, args));
            return @this;
        }

        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, string value)
        {
            if (condition) sb.AppendLine(value);
            return sb;
        }

        public static StringBuilder AppendLineIf<T>(this StringBuilder @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var value in values)
            {
                if (predicate(value))
                {
                    @this.AppendLine(value.ToString());
                }
            }
            return @this;
        }

        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, object value)
        {
            if (condition) sb.AppendLine(value.ToString());
            return sb;
        }

        public static StringBuilder AppendLineIf(this StringBuilder sb, bool condition, string format, params object[] args)
        {
            if (condition) sb.AppendFormat(format, args).AppendLine();
            return sb;
        }

        public static StringBuilder AppendLineJoin<T>(this StringBuilder @this, string separator, IEnumerable<T> values)
        {
            @this.AppendLine(string.Join(separator, values));
            return @this;
        }

        public static StringBuilder AppendLineJoin(this StringBuilder @this, string separator, params object[] values)
        {
            @this.AppendLine(string.Join(separator, values));
            return @this;
        }

        public static void AppendLine(this StringBuilder builder, string value, int counter, bool tab = true)
        {
            if (counter <= 0) builder.AppendLine(value);
            var space = tab ? new string('\t', counter) : new string(' ', counter);
            builder.AppendLine($"{space}{value}");
        }

        public static void Append(this StringBuilder builder, string value, int counter, bool tab = true)
        {
            if (counter <= 0) builder.Append(value);
            var space = tab ? new string('\t', counter) : new string(' ', counter);
            builder.Append($"{space}{value}");
        }

        public static StringBuilder Reverse(this StringBuilder sb)
        {
            var start = 0;
            var end = sb.Length - 1;
            while (start < end)
            {
                var temp = sb[start];
                sb[start] = sb[end];
                sb[end] = temp;
                start++;
                end--;
            }
            return sb;
        }

        public static string Strip(this StringBuilder sb)
        {
            for (int i = sb.Length - 1; i >= 0 && sb[i] == ' '; --i)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string Strip(this StringBuilder sb, int length)
        {
            if (length > sb.Length || length < 1)
            {
                return sb.ToString();
            }
            sb.Remove(sb.Length - length, length);
            return sb.ToString();
        }

        public static string Substring(this StringBuilder @this, int startIndex)
        {
            return @this.ToString(startIndex, @this.Length - startIndex);
        }

        public static string Substring(this StringBuilder @this, int startIndex, int length)
        {
            return @this.ToString(startIndex, length);
        }

        public static bool IsNotNull(this StringBuilder stringBuilder)
        {
            return !stringBuilder.IsNull();
        }
        public static bool IsNull(this StringBuilder stringBuilder)
        {
            return stringBuilder == null;
        }
        public static bool IsNotEmpty(this StringBuilder stringBuilder)
        {
            return !stringBuilder.IsEmpty();
        }
        public static bool IsEmpty(this StringBuilder stringBuilder)
        {
            return stringBuilder.ToString() == string.Empty;
        }
        public static bool IsNotNullOrEmpty(this StringBuilder stringBuilder)
        {
            return !stringBuilder.IsNullOrEmpty();
        }
        public static bool IsNullOrEmpty(this StringBuilder stringBuilder)
        {
            return stringBuilder == null || string.IsNullOrEmpty(stringBuilder.ToString());
        }
        public static bool IsNotNullOrWhiteSpace(this StringBuilder stringBuilder)
        {
            return !stringBuilder.IsNullOrWhiteSpace();
        }
        public static bool IsNullOrWhiteSpace(this StringBuilder stringBuilder)
        {
            return stringBuilder == null || string.IsNullOrWhiteSpace(stringBuilder.ToString());
        }
    }
}