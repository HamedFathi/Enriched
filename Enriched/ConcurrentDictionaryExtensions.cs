using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Enriched.ConcurrentDictionaryExtended
{
    public static class ConcurrentDictionaryExtensions
    {
        public static TValue AddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, Lazy<TValue>> @this, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return @this.AddOrUpdate(key, new Lazy<TValue>(() => addValueFactory(key), true), (k, v) => new Lazy<TValue>(() => updateValueFactory(k, v.Value), true)).Value;
        }

        public static void AddOrUpdate<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            @this.AddOrUpdate(key, value, (oldKey, oldValue) => value);
        }

        public static TValue GetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, Lazy<TValue>> @this, TKey key, Func<TKey, TValue> valueFactory)
        {
            return @this.GetOrAdd(key, new Lazy<TValue>(() => valueFactory(key), true)).Value;
        }

        public static TValue GetOrAdd<TKey, TValue>(this ConcurrentDictionary<TKey, Lazy<TValue>> @this, TKey key, TValue value)
        {
            return @this.GetOrAdd(key, new Lazy<TValue>(() => value, true)).Value;
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> @this)
        {
            return @this.ToDictionary(entry => entry.Key, entry => entry.Value);
        }
    }
}