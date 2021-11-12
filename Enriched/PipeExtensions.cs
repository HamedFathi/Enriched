using System;

namespace Enriched.Piping
{
    public static class PipeExtensions
    {
        public static U Pipe<T, U>(this T obj, Func<T, U> f)
        {
            return f(obj);
        }

        public static T Pipe<T>(this T obj, Action<T> f)
        {
            f(obj);
            return obj;
        }

        public static void PipeVoid<T>(this T obj, Action<T> f)
        {
            f(obj);
        }
    }
}