using Jal.Factory.Model;

namespace Jal.Factory.Fluent
{
    public class ObjectFactoryConfigurationCreateDescriptor<TTarget, TRestriction>
    {
        private readonly ObjectFactoryConfigurationItem _objectFactoryConfigurationItem;

        public ObjectFactoryConfigurationCreateDescriptor(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            _objectFactoryConfigurationItem = objectFactoryConfigurationItem;
        }

        public ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction
        {
            _objectFactoryConfigurationItem.ResultType = typeof(TResult);

            return new ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction>(_objectFactoryConfigurationItem);
        }

    }
}
