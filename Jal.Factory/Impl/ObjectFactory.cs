using System.Collections.Generic;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Locator.Interface;

namespace Jal.Factory.Impl
{
    public class ObjectFactory : IObjectFactory
    {
        private readonly IObjectFactoryConfigurationProvider _objectFactoryConfigurationProvider;

        private readonly IServiceLocator _serviceLocator;

        private readonly IObjectFactoryConfigurationSelector _objectFactoryConfigurationSelector;

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IServiceLocator serviceLocator, IObjectFactoryConfigurationSelector objectFactoryConfigurationSelector)
        {
            _objectFactoryConfigurationProvider = objectFactoryConfigurationProvider;

            _serviceLocator = serviceLocator;

            _objectFactoryConfigurationSelector = objectFactoryConfigurationSelector;
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance) where TResult : class
        {
            var name = ObjectFactorySettings.BuildDefaultName(typeof(TTarget));

            return Create<TTarget, TResult>(instance, name);
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance, string name) where TResult : class
        {
            var factoryConfigurationItems = _objectFactoryConfigurationProvider.Provide(instance, name);

            var list = new List<TResult>();

            if (factoryConfigurationItems != null)
            {
                foreach (var configurationItem in factoryConfigurationItems)
                {
                    if (typeof(TResult).IsAssignableFrom(configurationItem.ResultType))
                    {
                        var result = _serviceLocator.Resolve<TResult>(configurationItem.ResultType.FullName);

                        if (_objectFactoryConfigurationSelector.Select(configurationItem, instance, result))
                        {
                            list.Add(result);
                        }
                    }
                }
            }
            return list.ToArray();
        }
    }
}
