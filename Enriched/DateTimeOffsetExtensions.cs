using Enriched.DateTimeExtended;
using System;

namespace Enriched.DateTimeOffsetExtended
{
    public static class DateTimeOffsetExtensions
    {
        public static bool Between(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
        {
            return minValue.CompareTo(@this) == -1 && @this.CompareTo(maxValue) == -1;
        }

        public static DateTimeOffset ConvertTime(this DateTimeOffset dateTimeOffset, TimeZoneInfo destinationTimeZone)
        {
            return TimeZoneInfo.ConvertTime(dateTimeOffset, destinationTimeZone);
        }

        public static DateTimeOffset ConvertTimeBySystemTimeZoneId(this DateTimeOffset dateTimeOffset, string destinationTimeZoneId)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(dateTimeOffset, destinationTimeZoneId);
        }

        public static bool In(this DateTimeOffset @this, params DateTimeOffset[] values)
        {
            return Array.IndexOf(values, @this) != -1;
        }

        public static bool InRange(this DateTimeOffset @this, DateTimeOffset minValue, DateTimeOffset maxValue)
        {
            return @this.CompareTo(minValue) >= 0 && @this.CompareTo(maxValue) <= 0;
        }

        public static bool NotIn(this DateTimeOffset @this, params DateTimeOffset[] values)
        {
            return Array.IndexOf(values, @this) == -1;
        }

        public static DateTimeOffset SetTime(this DateTimeOffset current, int hour)
        {
            return SetTime(current, hour, 0, 0, 0);
        }

        public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute)
        {
            return SetTime(current, hour, minute, 0, 0);
        }

        public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute, int second)
        {
            return SetTime(current, hour, minute, second, 0);
        }

        public static DateTimeOffset SetTime(this DateTimeOffset current, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(current.Year, current.Month, current.Day, hour, minute, second, millisecond);
        }

        public static DateTimeOffset SetTime(this DateTimeOffset date, TimeSpan time)
        {
            return date.SetTime(time, null);
        }

        public static DateTimeOffset SetTime(this DateTimeOffset date, TimeSpan time, TimeZoneInfo localTimeZone)
        {
            var localDate = date.ToLocalDateTime(localTimeZone);
            localDate.SetTime(time);
            return localDate.ToDateTimeOffset(localTimeZone);
        }

        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeUtc)
        {
            return dateTimeUtc.ToLocalDateTime(null);
        }

        public static DateTime ToLocalDateTime(this DateTimeOffset dateTimeUtc, TimeZoneInfo localTimeZone)
        {
            localTimeZone = localTimeZone ?? TimeZoneInfo.Local;
            return TimeZoneInfo.ConvertTime(dateTimeUtc, localTimeZone).DateTime;
        }

        public static DateTimeOffset TruncateToHours(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, 0, 0, 0, date.Offset);
        }

        public static DateTimeOffset TruncateToMilliseconds(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, date.Millisecond, date.Offset);
        }

        public static DateTimeOffset TruncateToMinutes(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0, 0, date.Offset);
        }

        public static DateTimeOffset TruncateToSeconds(this DateTimeOffset date)
        {
            return new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second, 0, date.Offset);
        }
    }
}