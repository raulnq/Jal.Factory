using System.Collections.Generic;
using Jal.Factory.Fluent;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class ObjectFactory : IObjectFactory
    {

        public static IObjectFactory Current;

        public static IObjectCreatorSetupDescriptor Setup
        {
            get
            {
                return new SetupDescriptor();
            }
        }

        public IObjectFactoryConfigurationProvider ConfigurationProvider { get; set; }

        public IObjectFactoryConfigurationRuntimeProvider ConfigurationRuntimeProvider { get; set; }

        public IObjectCreator Creator { get; set; }

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IObjectCreator objectCreator, IObjectFactoryConfigurationRuntimeProvider objectFactoryConfigurationRuntimeProvider)
        {
            ConfigurationProvider = objectFactoryConfigurationProvider;

            Creator = objectCreator;

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
                        var result = Creator.Create<TResult>(configurationItem.ResultType.FullName);

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
