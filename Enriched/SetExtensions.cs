using System.Collections.Generic;
using System.Linq;

namespace Enriched.SetExtended
{
    public static class SetExtensions
    {
        public static int AddRange<T>(this ISet<T> source, IEnumerable<T> items)
        {
            return items.Count(source.Add);
        }
    }
}