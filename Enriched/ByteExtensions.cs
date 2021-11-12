namespace Enriched.ByteExtended
{
    public static class ByteExtensions
    {
        public static string ToHexString(this byte @byte)
        {
            return @byte.ToString("x2");
        }
    }
}