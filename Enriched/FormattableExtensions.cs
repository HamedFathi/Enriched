using System;
using System.Globalization;

namespace Enriched.FormattableExtended
{
    public static class FormattableExtensions
    {
        public static string ToInvariantString(this IFormattable value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string ToString(this IFormattable value, IFormatProvider provider)
        {
            return value.ToString(null, provider);
        }
    }
}