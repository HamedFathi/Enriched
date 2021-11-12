using System.Collections.Generic;

namespace Enriched
{
    public class GroupJoinResult<TKey, TValue>
    {
        public TKey Key { get; set; }
        public IEnumerable<TValue> Values { get; set; }
    }
}