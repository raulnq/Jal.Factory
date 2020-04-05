using System;
using System.Collections.Generic;
using Jal.Factory.Fluent.Impl;

namespace Jal.Factory
{
    public abstract class AbstractObjectFactoryConfigurationSource : IObjectFactoryConfigurationSource
    {
        protected readonly List<ObjectFactoryConfigurationItem> Items = new List<ObjectFactoryConfigurationItem>();

        public ObjectFactoryConfiguration Source()
        {
            var result = new ObjectFactoryConfiguration();

            foreach (var item in Items)
            {
                result.Items.Add(item);
            }

            return result;
        }

        public IObjectFactoryConfigurationCreateBuilder<TTarget, TService> For<TTarget, TService>()
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget), typeof(TService), string.Empty);

            var descriptor = new ObjectFactoryConfigurationCreateBuilder<TTarget, TService>(value);

            Items.Add(value);

            return descriptor;
        }

        public void For<TTarget, TService>(string name, Action<IObjectFactoryConfigurationGroupCreateBuilder<TTarget, TService>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var descriptor = new ObjectFactoryConfigurationGroupCreateBuilder<TTarget, TService>(Items, name);

            action.Invoke(descriptor);
        }
    }     
}
