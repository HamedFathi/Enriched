using System.ComponentModel;

namespace Enriched.ComponentExtended
{
    public static class ComponentExtensions
    {
        public static bool IsInDesignMode(this IComponent target)
        {
            var site = target.Site;
            return site != null && site.DesignMode;
        }

        public static bool IsInRuntimeMode(this IComponent target)
        {
            return !IsInDesignMode(target);
        }
    }
}