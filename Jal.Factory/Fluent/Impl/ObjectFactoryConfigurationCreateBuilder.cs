using Jal.Factory.Fluent.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationCreateBuilder<TTarget, TRestriction> : IObjectFactoryConfigurationCreateBuilder<TTarget, TRestriction>
    {
        private readonly ObjectFactoryConfigurationItem _item;

        public ObjectFactoryConfigurationCreateBuilder(ObjectFactoryConfigurationItem item)
        {
            _item = item;
        }

        public IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TResult>() where TResult : TRestriction
        {
            _item.ResultType = typeof(TResult);

            return new ObjectFactoryConfigurationWhenBuilder<TTarget>(_item);
        }

    }
}
