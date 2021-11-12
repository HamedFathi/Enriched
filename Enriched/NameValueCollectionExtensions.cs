using System.Collections.Generic;
using System.Collections.Specialized;

namespace Enriched.NameValueCollectionExtended
{
    public static class NameValueCollectionExtensions
    {
        public static IDictionary<string, object> ToDictionary(this NameValueCollection @this)
        {
            var dict = new Dictionary<string, object>();

            if (@this == null) return dict;

            foreach (var key in @this.AllKeys)
            {
                dict.Add(key, @this[key]);
            }

            return dict;
        }

        public static IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs(this NameValueCollection collection)
        {
            if (collection is null)
            {
                throw new System.ArgumentNullException(nameof(collection));
            }

            foreach (string key in collection.Keys)
            {
                yield return new KeyValuePair<string, string>(key, collection[key]);
            }
        }
    }
}