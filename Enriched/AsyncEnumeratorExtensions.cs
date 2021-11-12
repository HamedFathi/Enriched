#if NETSTANDARD2_1
using System.Collections.Generic;

public static class AsyncEnumeratorExtensions
{
    public static IAsyncEnumerator<T> GetAsyncEnumerator<T>(this IAsyncEnumerator<T> enumerator) => enumerator;
}
#endif