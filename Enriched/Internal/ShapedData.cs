using System.Collections.Generic;

namespace Enriched
{
    public class ShapedData
    {
        public List<List<DataField>> Values { get; set; }

        public ShapedData()
        {
            Values = new List<List<DataField>>();
        }
    }
}