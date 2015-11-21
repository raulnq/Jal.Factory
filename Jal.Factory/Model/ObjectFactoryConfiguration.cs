using System.Collections.Generic;
using System.Diagnostics;

namespace Jal.Factory.Model
{
    [DebuggerDisplay("Items: {Items.Count}")]
    public class ObjectFactoryConfiguration
    {
        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public List<ObjectFactoryConfigurationItem> Items { get; set; }

        public ObjectFactoryConfiguration()
        {
            Items = new List<ObjectFactoryConfigurationItem>();
        }
    }
}
