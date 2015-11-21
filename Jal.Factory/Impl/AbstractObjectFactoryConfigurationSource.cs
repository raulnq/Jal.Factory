using System;
using System.Collections.Generic;
using Jal.Factory.Fluent;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public abstract class AbstractObjectFactoryConfigurationSource : IObjectFactoryConfigurationSource
    {
        protected readonly List<ObjectFactoryConfigurationItem> ObjectFactoryConfigurationItems = new List<ObjectFactoryConfigurationItem>();

        public ObjectFactoryConfiguration Source()
        {
            var result = new ObjectFactoryConfiguration();

            foreach (var item in ObjectFactoryConfigurationItems)
            {
                result.Items.Add(item);
            }

            return result;
        }

        public ObjectFactoryConfigurationCreateDescriptor<TTarget, TRestriction> For<TTarget, TRestriction>()
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget));

            var descriptor = new ObjectFactoryConfigurationCreateDescriptor<TTarget, TRestriction>(value);

            ObjectFactoryConfigurationItems.Add(value);

            return descriptor;
        }

        public void For<TTarget, TRestriction>(string name, Action<ObjectFactoryConfigurationCreateGroupDescriptor<TTarget, TRestriction>> action)
        {
            var descriptor = new ObjectFactoryConfigurationCreateGroupDescriptor<TTarget, TRestriction>(ObjectFactoryConfigurationItems, name);

            action(descriptor);
        }
    }     
}
