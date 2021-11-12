using System.Reflection;

namespace Enriched
{
    internal class PropertyMap
    {
        internal PropertyInfo SourceProperty { get; set; }
        internal PropertyInfo DestinationProperty { get; set; }
    }
}