using System;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> : IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> where TResult : TRestriction
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ObjectFactoryConfigurationWhenFluentBuilder(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }

        public IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> When(Func<TTarget, TRestriction, bool> filter)
        {
            _objectFactoryConfigurationItem.Filter = filter;

            return this;
        }

        public IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> When(Func<TTarget, bool> selector)
        {
            _objectFactoryConfigurationItem.Selector = selector;

            return this;
        }
    }
}
