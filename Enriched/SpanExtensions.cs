using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Enriched.SpanExtended
{
    public static class SpanExtensions
    {
        public static Span<T> AsSpan<T>(this List<T> list)
        {
            return CollectionsMarshal.AsSpan(list);
        }
    }
}
