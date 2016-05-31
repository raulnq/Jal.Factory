using Jal.Factory.Fluent.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationFluentBuilder<TTarget, TRestriction> : IObjectFactoryConfigurationFluentBuilder<TTarget, TRestriction>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ObjectFactoryConfigurationFluentBuilder(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }

        public IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction
        {
            _objectFactoryConfigurationItem.ResultType = typeof(TResult);

            return new ObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction>(_objectFactoryConfigurationItem);
        }

    }
}
