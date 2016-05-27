using System.Collections.Generic;
using Jal.Factory.Fluent;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Locator.Interface;

namespace Jal.Factory.Impl
{
    public class ObjectFactory : IObjectFactory
    {

        public static IObjectFactory Current;

        public static IObjectFactoryServiceLocatorSetupDescriptor Setup
        {
            get
            {
                return new ObjectFactorySetupDescriptor();
            }
        }

        public IObjectFactoryConfigurationProvider ConfigurationProvider { get; set; }

        public IObjectFactoryConfigurationRuntimeProvider ConfigurationRuntimeProvider { get; set; }

        private readonly IServiceLocator _serviceLocator;

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IServiceLocator serviceLocator, IObjectFactoryConfigurationRuntimeProvider objectFactoryConfigurationRuntimeProvider)
        {
            ConfigurationProvider = objectFactoryConfigurationProvider;

            _serviceLocator = serviceLocator;

            ConfigurationRuntimeProvider = objectFactoryConfigurationRuntimeProvider;
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance) where TResult : class
        {
            var name = ObjectFactorySettings.BuildDefaultName(typeof(TTarget));

            return Create<TTarget, TResult>(instance, name);
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance, string name) where TResult : class
        {
            var factoryConfigurationItems = ConfigurationProvider.Provide(instance, name);

            var list = new List<TResult>();

            if (factoryConfigurationItems != null)
            {
                foreach (var configurationItem in factoryConfigurationItems)
                {
                    if (typeof(TResult).IsAssignableFrom(configurationItem.ResultType))
                    {
                        var result = _serviceLocator.Resolve<TResult>(configurationItem.ResultType.FullName);

                        if (ConfigurationRuntimeProvider.Provide(configurationItem, instance, result))
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
