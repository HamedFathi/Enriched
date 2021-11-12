using Enriched.DataRowExtended;
using Enriched.EnumerableExtended;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Enriched.DataTableExtended
{
    public static class DataTableExtensions
    {
        internal static readonly IReadOnlyList<Type> SupportedDataTypes = new[]
        {
            typeof(bool),
            typeof(byte),
            typeof(char),
            typeof(DateTime),
            typeof(decimal),
            typeof(double),
            typeof(Guid),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(sbyte),
            typeof(float),
            typeof(string),
            typeof(TimeSpan),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(byte[])
        };

        public static DataRow FirstRow(this DataTable @this)
        {
            if (!@this.HasRows()) throw new ArgumentException(nameof(@this));
            return @this.Rows[0];
        }

        public static Type GetColumnDataType(this DataTable @this, string columnName)
        {
            if (!@this.Columns.Contains(columnName)) throw new ArgumentException($"DataTable Column:{columnName}");
            return @this.Columns[columnName].DataType;
        }

        public static Type GetColumnDataType(this DataTable @this, int index)
        {
            if (index < 0) throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} out of range");
            if (@this.Columns.Count - 1 < index) throw new ArgumentOutOfRangeException(nameof(index), $"{nameof(index)} out of range");
            return @this.Columns[index].DataType;
        }

        public static bool HasRows(this DataTable @this)
        {
            return @this.Rows.Count > 0;
        }

        public static DataRow LastRow(this DataTable @this)
        {
            if (!@this.HasRows()) throw new ArgumentException(nameof(@this));
            return @this.Rows[@this.Rows.Count - 1];
        }

        public static IEnumerable<DataColumn> ToDataColumns(this DataTable @this)
        {
            return @this.Columns.Cast<DataColumn>();
        }

        public static IEnumerable<DataRow> ToDataRows(this DataTable @this)
        {
            return @this.Rows.Cast<DataRow>();
        }

        public static IEnumerable<dynamic> ToDynamicObjects(this DataTable @this)
        {
            return @this.ToDataRows().Select(row => row.ToDynamicObject());
        }

        public static IEnumerable<T> ToEntities<T>(this DataTable @this) where T : class, new()
        {
            return @this.ToDataRows().Select(s => s.ToEntity<T>()).ToList();
        }
    }
}