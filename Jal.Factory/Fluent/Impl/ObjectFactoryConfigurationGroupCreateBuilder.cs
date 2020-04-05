using System.Collections.Generic;

namespace Jal.Factory
{
    public class ObjectFactoryConfigurationGroupCreateBuilder<TTarget, TService> : IObjectFactoryConfigurationGroupCreateBuilder<TTarget, TService>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _name;

        public ObjectFactoryConfigurationGroupCreateBuilder(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string name)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _name = name;
        }

        public IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TImplementation>() where TImplementation : TService
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget), typeof(TImplementation), typeof(TService), _name);

            _objectFactoryConfigurationItems.Add(value);

            return new ObjectFactoryConfigurationWhenBuilder<TTarget>(value);
        }

    }
}