using System;
using System.Collections.Generic;
using Jal.Factory.Fluent.Impl;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
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

        public IObjectFactoryConfigurationCreateBuilder<TTarget, TRestriction> For<TTarget, TRestriction>()
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget));

            var descriptor = new ObjectFactoryConfigurationCreateBuilder<TTarget, TRestriction>(value);

            Items.Add(value);

            return descriptor;
        }

        public void For<TTarget, TRestriction>(string name, Action<IObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction>> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            var descriptor = new ObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction>(Items, name);

            action.Invoke(descriptor);
        }
    }     
}
