namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationCreateBuilder<TTarget, TService> : IObjectFactoryConfigurationCreateBuilder<TTarget, TService>
    {
        private readonly ObjectFactoryConfigurationItem _item;

        public ObjectFactoryConfigurationCreateBuilder(ObjectFactoryConfigurationItem item)
        {
            _item = item;
        }

        public IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TImplementation>() where TImplementation : TService
        {
            _item.ImplementationType = typeof(TImplementation);

            return new ObjectFactoryConfigurationWhenBuilder<TTarget>(_item);
        }

    }
}
