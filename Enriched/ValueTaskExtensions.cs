#if NETSTANDARD2_1
using System;

namespace Enriched.ValueTaskExtended
{
    public static class ValueTaskExtensions
    {
        public static async void SafeFireAndForget(this System.Threading.Tasks.ValueTask @this, bool continueOnCapturedContext = true, Action<Exception> onException = null)
        {
            try
            {
                await @this.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception e) when (onException != null)
            {
                onException(e);
            }
        }
    }
}
#endif