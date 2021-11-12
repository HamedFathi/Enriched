using System.Diagnostics;

namespace Enriched.StopwatchExtended
{
    public static class StopwatchExtensions
    {
        public static long ElapsedSeconds(this Stopwatch sw)
        {
            return sw.ElapsedMilliseconds / 1000;
        }
    }
}