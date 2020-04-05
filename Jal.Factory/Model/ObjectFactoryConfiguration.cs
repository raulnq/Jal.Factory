using System.Collections.Generic;
using System.Diagnostics;

namespace Jal.Factory
{
    [DebuggerDisplay("Items: {Items.Count}")]
    public class ObjectFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public List<ObjectFactoryConfigurationItem> Items { get; }

        public ObjectFactoryConfiguration(List<ObjectFactoryConfigurationItem> items)
        {
            Items = items;
        }

        public ObjectFactoryConfiguration()
        {
            Items = new List<ObjectFactoryConfigurationItem>();
        }
    }
}
