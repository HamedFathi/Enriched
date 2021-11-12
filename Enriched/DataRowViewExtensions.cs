using Enriched.ObjectExtended;
using Enriched.StringExtended;
using System;        
using System.Data;

namespace Enriched.DataRowViewExtended
{
    public static class DataRowViewExtensions
    {
        public static T Get<T>(this DataRowView row, string field)
        {
            return row.Get(field, default(T));
        }

        public static T Get<T>(this DataRowView row, string field, T defaultValue)
        {
            var value = row[field];
            if (value == DBNull.Value)
                return defaultValue;
            return value.ConvertTo(defaultValue);
        }

        public static bool GetBoolean(this DataRowView row, string field)
        {
            return row.GetBoolean(field, false);
        }

        public static bool GetBoolean(this DataRowView row, string field, bool defaultValue)
        {
            var value = row[field];
            return value is bool b ? b : defaultValue;
        }

        public static byte[] GetBytes(this DataRowView row, string field)
        {
            return row[field] as byte[];
        }

        public static DateTime GetDateTime(this DataRowView row, string field)
        {
            return row.GetDateTime(field, DateTime.MinValue);
        }

        public static DateTime GetDateTime(this DataRowView row, string field, DateTime defaultValue)
        {
            var value = row[field];
            return value is DateTime time ? time : defaultValue;
        }

        public static DateTimeOffset GetDateTimeOffset(this DataRowView row, string field)
        {
            return new DateTimeOffset(row.GetDateTime(field), TimeSpan.Zero);
        }

        public static DateTimeOffset GetDateTimeOffset(this DataRowView row, string field, DateTimeOffset defaultValue)
        {
            var dt = row.GetDateTime(field);
            return dt != DateTime.MinValue ? new DateTimeOffset(dt, TimeSpan.Zero) : defaultValue;
        }

        public static decimal GetDecimal(this DataRowView row, string field)
        {
            return row.GetDecimal(field, 0);
        }

        public static decimal GetDecimal(this DataRowView row, string field, long defaultValue)
        {
            var value = row[field];
            return value is decimal value1 ? value1 : defaultValue;
        }

        public static Guid GetGuid(this DataRowView row, string field)
        {
            var value = row[field];
            return value is Guid guid ? guid : Guid.Empty;
        }

        public static int GetInt32(this DataRowView row, string field)
        {
            return row.GetInt32(field, 0);
        }

        public static int GetInt32(this DataRowView row, string field, int defaultValue)
        {
            var value = row[field];
            return value is int i ? i : defaultValue;
        }

        public static long GetInt64(this DataRowView row, string field)
        {
            return row.GetInt64(field, 0);
        }

        public static long GetInt64(this DataRowView row, string field, int defaultValue)
        {
            var value = row[field];
            return value is long l ? l : defaultValue;
        }

        public static string GetString(this DataRowView row, string field)
        {
            return row.GetString(field, null);
        }

        public static string GetString(this DataRowView row, string field, string defaultValue)
        {
            var value = row[field];
            return value is string s ? s : defaultValue;
        }

        public static Type GetType(this DataRowView row, string field)
        {
            return row.GetType(field, null);
        }

        public static Type GetType(this DataRowView row, string field, Type defaultValue)
        {
            var classType = row.GetString(field);
            if (classType.IsNotEmpty())
            {
                var type = Type.GetType(classType);
                if (type != null)
                    return type;
            }
            return defaultValue;
        }

        public static object GetTypeInstance(this DataRowView row, string field)
        {
            return row.GetTypeInstance(field, null);
        }

        public static object GetTypeInstance(this DataRowView row, string field, Type defaultValue)
        {
            var type = row.GetType(field, defaultValue);
            return type != null ? Activator.CreateInstance(type) : null;
        }

        public static T GetTypeInstance<T>(this DataRowView row, string field) where T : class
        {
            return row.GetTypeInstance(field, null) as T;
        }

        public static T GetTypeInstanceSafe<T>(this DataRowView row, string field, Type type) where T : class
        {
            var instance = row.GetTypeInstance(field, null) as T;
            return instance ?? Activator.CreateInstance(type) as T;
        }

        public static T GetTypeInstanceSafe<T>(this DataRowView row, string field) where T : class, new()
        {
            var instance = row.GetTypeInstance(field, null) as T;
            return instance ?? new T();
        }

        public static bool IsDbNull(this DataRowView row, string field)
        {
            var value = row[field];
            return value == DBNull.Value;
        }
    }
}