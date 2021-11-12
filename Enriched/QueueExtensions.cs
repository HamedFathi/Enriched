using System;
using System.Collections.Generic;

namespace Enriched.QueueExtended
{
    public static class QueueExtensions
    {
        public static IEnumerable<TSource> DequeueAsEnumerable<TSource>(
                    this Queue<TSource> source)
        {
            while (source.Count != 0)
                yield return source.Dequeue();
        }

        public static T DequeueOrDefault<T>(this Queue<T> self)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            return (self.Count > 0) ? self.Dequeue() : default;
        }

        public static void EnqueueRange<T>(this Queue<T> queue, params T[] items)
        {
            foreach (var item in items)
                queue.Enqueue(item);
        }

        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            foreach (var item in items)
                queue.Enqueue(item);
        }

        public static bool TryDequeue<T>(this Queue<T> self, out T element)
        {
            if (self == null)
                throw new ArgumentNullException(nameof(self));

            element = default;

            if (self.Count > 0)
            {
                element = self.Dequeue();
                return true;
            }

            return false;
        }
    }
}