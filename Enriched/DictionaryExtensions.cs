using Enriched.EnumerableExtended;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Dynamic;
using System.Linq;

namespace Enriched.DictionaryExtended
{
    public static class DictionaryExtensions
    {
        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, value);
                return true;
            }

            return false;
        }

        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, valueFactory());
                return true;
            }

            return false;
        }

        public static bool AddIfNotContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(key, valueFactory(key));
                return true;
            }

            return false;
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }
            else
            {
                @this[key] = value;
            }

            return @this[key];
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, addValue));
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }

            return @this[key];
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, addValueFactory(key)));
            }
            else
            {
                @this[key] = updateValueFactory(key, @this[key]);
            }

            return @this[key];
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> container,
                                                                                                Func<TValue, TKey> keyProducerFunc, IEnumerable<TValue> rangeToAdd)
        {
            if ((container == null) || (rangeToAdd == null))
            {
                return;
            }
            if (keyProducerFunc == null)
            {
                throw new ArgumentNullException(nameof(keyProducerFunc));
            }
            foreach (var toAdd in rangeToAdd)
            {
                container[keyProducerFunc(toAdd)] = toAdd;
            }
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<KeyValuePair<TKey, TValue>> pairs)
        {
            foreach (var pair in pairs) @this.Add(pair);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> @this, params KeyValuePair<TKey, TValue>[] pairs)
        {
            foreach (var pair in pairs) @this.Add(pair);
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                if (source.ContainsKey(kvp.Key))
                {
                    throw new ArgumentException("An item with the same key has already been added.");
                }
                source.Add(kvp);
            }
        }

        public static void AddRangeUnique<TKey, TValue>(this IDictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                if (!source.ContainsKey(kvp.Key))
                {
                    source.Add(kvp);
                }
            }
        }

        public static IDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return new ReadOnlyDictionary<TKey, TValue>(dictionary);
        }

        public static IDictionary<TValue, TKey> ConcatAllToDictionary<TValue, TKey>(this IDictionary<TValue, TKey> dictionary, params IDictionary<TValue, TKey>[] dictionaries)
        {
            var result = dictionary;
            dictionaries.ForEach(x =>
            {
                if (x == null)
                    return;
                x.ForEach(y => result.Add(y.Key, y.Value));
            });
            return result;
        }

        public static IDictionary<TValue, TKey> ConcatAllToDictionarySafe<TValue, TKey>(this IDictionary<TValue, TKey> dictionary, params IDictionary<TValue, TKey>[] dictionaries)
        {
            var result = dictionary;
            dictionaries.ForEach(x =>
            {
                if (x == null)
                    return;
                x.ForEach(y =>
                {
                    if (!result.ContainsKey(y.Key))
                        result.Add(y.Key, y.Value);
                });
            });
            return result;
        }

        public static IDictionary<TValue, TKey> ConcatToDictionary<TValue, TKey>(this IEnumerable<KeyValuePair<TValue, TKey>> first, IEnumerable<KeyValuePair<TValue, TKey>> second)
        {
            return first
                   .Concat(second)
                   .ToDictionary(x => x.Key, x => x.Value);
        }

        public static IDictionary<TValue, TKey> ConcatToDictionarySafe<TValue, TKey>(this IEnumerable<KeyValuePair<TValue, TKey>> first, IEnumerable<KeyValuePair<TValue, TKey>> second)
        {
            return first
                   .Concat(second)
                   .GroupBy(x => x.Key)
                   .ToDictionary(x => x.Key,
                                  x => x.First()
                                        .Value);
        }

        public static bool ContainsAllKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (!@this.ContainsKey(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (@this.ContainsKey(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsKeyAll<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (keys == null) throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null");
            return @this.ContainsKeyAll(keys);
        }

        public static bool ContainsKeyAll<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<TKey> keys)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (keys == null) throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null");
            return keys.All(@this.ContainsKey);
        }

        public static bool ContainsKeyAny<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TKey[] keys)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (keys == null) throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null");
            return @this.ContainsKeyAny(keys);
        }

        public static bool ContainsKeyAny<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<TKey> keys)
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (keys == null) throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null");
            return keys.Any(@this.ContainsKey);
        }

        public static bool ContainsSameKeys<TKey, TValue>(this IDictionary<TKey, TValue> from, IDictionary<TKey, TValue> to)
        {
            var search = to
               .Keys
               .Where(p => from.Keys.Contains(p));
            return search.Count() == from.Count;
        }

        public static bool ContainsValue<TKey, TValue>(this IDictionary<TKey, TValue> @this, TValue value) where TValue : class
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (value == null) throw new ArgumentNullException(nameof(value), $"{nameof(value)} is null");
            return @this.Values.Contains(value);
        }

        public static bool ContainsValueAll<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TValue[] values) where TValue : class
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (values == null) throw new ArgumentNullException(nameof(values), $"{nameof(values)} is null");
            return @this.ContainsValueAll(values);
        }

        public static bool ContainsValueAll<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<TValue> values) where TValue : class
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (values == null) throw new ArgumentNullException(nameof(values), $"{nameof(values)} is null");
            return values.All(@this.ContainsValue);
        }

        public static bool ContainsValueAny<TKey, TValue>(this IDictionary<TKey, TValue> @this, params TValue[] values) where TValue : class
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (values == null) throw new ArgumentNullException(nameof(values), $"{nameof(values)} is null");
            return @this.ContainsValueAny(values);
        }

        public static bool ContainsValueAny<TKey, TValue>(this IDictionary<TKey, TValue> @this, IEnumerable<TValue> values) where TValue : class
        {
            if (@this == null) throw new ArgumentNullException(nameof(@this), $"{nameof(@this)} is null");
            if (values == null) throw new ArgumentNullException(nameof(values), $"{nameof(values)} is null");
            return values.Any(@this.ContainsValue);
        }

        public static IEnumerable<TKey> GetAllKeys<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary is null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.Select(x => x.Key);
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, TValue value)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }

            return @this[key];
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!@this.ContainsKey(key))
            {
                @this.Add(new KeyValuePair<TKey, TValue>(key, valueFactory(key)));
            }

            return @this[key];
        }

        public static V GetOrLookUp<K, V>(this IDictionary<K, V> dictionary, K key, Func<K, V> lookup)
        {
            if (!dictionary.TryGetValue(key, out V value))
            {
                value = lookup(key);
                dictionary.Add(key, value);
            }

            return value;
        }

        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue toReturn;
            if ((key == null) || !dictionary.TryGetValue(key, out toReturn))
            {
                toReturn = default;
            }
            return toReturn;
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            return dictionary.GetValueOrDefault(key, default);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
        {
            if (dictionary.TryGetValue(key, out TValue value))
            {
                return value;
            }
            return defaultValue;
        }

        public static IDictionary<TValue, TKey> Invert<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            return dictionary.ToDictionary(pair => pair.Value, pair => pair.Key);
        }

        public static bool IsEmpty(this IDictionary @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            return @this.Count == 0;
        }

        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            return @this.Count == 0;
        }

        public static bool IsNullOrEmpty(this IDictionary @this)
        {
            if (@this == null) return true;
            return @this.Count == 0;
        }

        public static bool IsNullOrEmpty<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            if (@this == null) return true;
            return @this.Count == 0;
        }

        public static void Merge<TKey, TValue>(this IDictionary<TKey, TValue> target, IDictionary<TKey, TValue> destination)
        {
            foreach (var item in destination)
            {
                if (!target.ContainsKey(item.Key))
                {
                    target.Add(item.Key, item.Value);
                }
            }
        }

        public static void RemoveIfContainsKey<TKey, TValue>(this IDictionary<TKey, TValue> @this, TKey key)
        {
            if (@this.ContainsKey(key))
            {
                @this.Remove(key);
            }
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            return new SortedDictionary<TKey, TValue>(dictionary);
        }

        public static IDictionary<TKey, TValue> Sort<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public static IDictionary<K, V> SortByKey<K, V>(this IDictionary<K, V> dictionary, bool ascending = true)
        {
            return ascending
                ? dictionary.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value)
                : dictionary.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
        }

        public static IDictionary<K, V> SortByValue<K, V>(this IDictionary<K, V> dictionary, bool ascending = true)
        {
            return ascending
                ? dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value)
                : dictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        }

        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            var cDictionary = new ConcurrentDictionary<TKey, TValue>();

            foreach (var item in @this)
            {
                cDictionary.AddOrUpdate(item.Key, item.Value);
            }

            return cDictionary;
        }

        public static ExpandoObject ToExpando(this IDictionary<string, object> @this)
        {
            var expando = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)expando;
            foreach (var item in @this)
            {
                if (item.Value is IDictionary<string, object> d)
                {
                    expandoDict.Add(item.Key, d.ToExpando());
                }
                else
                {
                    expandoDict.Add(item);
                }
            }
            return expando;
        }

        public static Hashtable ToHashtable(this IDictionary @this)
        {
            return new Hashtable(@this);
        }

        public static Hashtable ToHashTable<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            var table = new Hashtable();
            foreach (var item in dictionary)
                table.Add(item.Key, item.Value);

            return table;
        }

        public static NameValueCollection ToNameValueCollection(this IDictionary<string, string> @this)
        {
            if (@this == null)
                return null;

            var nameValueCollection = new NameValueCollection();
            foreach (var item in @this) nameValueCollection.Add(item.Key, item.Value);
            return nameValueCollection;
        }

        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> @this)
        {
            return new SortedDictionary<TKey, TValue>(@this);
        }

        public static SortedDictionary<TKey, TValue> ToSortedDictionary<TKey, TValue>(this IDictionary<TKey, TValue> @this, IComparer<TKey> comparer)
        {
            return new SortedDictionary<TKey, TValue>(@this, comparer);
        }

        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> self, TKey key, out TValue value)
        {
            if (self is null)
            {
                throw new ArgumentNullException(nameof(self));
            }

            return self.TryGetValue(key, out value) && self.Remove(key);
        }
    }
}