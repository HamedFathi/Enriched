using System;
using System.Collections.Generic;
using System.Linq;

namespace Enriched
{
    internal class ObjectMapper : ObjectCopyBase
    {
        private readonly Dictionary<string, PropertyMap[]> _maps = new Dictionary<string, PropertyMap[]>();

        internal override void MapTypes(Type source, Type target)
        {
            var key = GetMapKey(source, target);
            if (_maps.ContainsKey(key))
            {
                return;
            }

            var props = GetMatchingProperties(source, target);
            _maps.Add(key, props.ToArray());
        }

        internal override void Copy(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();

            var key = GetMapKey(sourceType, targetType);
            if (!_maps.ContainsKey(key))
            {
                MapTypes(sourceType, targetType);
            }

            var propMap = _maps[key];

            for (var i = 0; i < propMap.Length; i++)
            {
                var prop = propMap[i];
                var sourceValue = prop.SourceProperty.GetValue(source, null);
                prop.DestinationProperty.SetValue(target, sourceValue, null);
            }
        }
    }
}