using System.Collections.Generic;
using System.Data;

namespace Enriched.DataColumnCollectionExtended
{
    public static class DataColumnCollectionExtensions
    {
        public static void AddRange(this DataColumnCollection @this, IEnumerable<string> columnNames)
        {
            foreach (var columnName in columnNames) @this.Add(columnName);
        }

        public static void AddRange(this DataColumnCollection @this, params string[] columnNames)
        {
            foreach (var columnName in columnNames) @this.Add(columnName);
        }
    }
}