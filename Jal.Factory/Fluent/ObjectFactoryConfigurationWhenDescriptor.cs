using System;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent
{
    public class ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction>  where TResult : TRestriction
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ObjectFactoryConfigurationWhenDescriptor(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }

        public ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction> When(Func<TTarget, TRestriction, bool> filter)
        {
            _objectFactoryConfigurationItem.Filter = filter;

            return this;
        }

        public ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction> When(Func<TTarget, bool> selector)
        {
            _objectFactoryConfigurationItem.Selector = selector;

            return this;
        }
    } 
}
