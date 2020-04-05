using System;

namespace Jal.Factory
{
    public class ObjectFactoryConfigurationWhenBuilder<TTarget> : IObjectFactoryConfigurationWhenBuilder<TTarget>
    {
        private readonly ObjectFactoryConfigurationItem _item;

        public ObjectFactoryConfigurationWhenBuilder(ObjectFactoryConfigurationItem item)
        {
            _item = item;
        }

        public void When(Func<TTarget, bool> selector)
        {
            _item.Selector = selector;
        }
    }
}
