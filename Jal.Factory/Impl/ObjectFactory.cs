using System;
using System.Linq;

namespace Jal.Factory
{
    public class ObjectFactory : IObjectFactory
    {
        public IObjectFactoryConfigurationProvider ConfigurationProvider { get; }

        public IObjectFactoryInterceptor Interceptor { get; set; }

        public IObjectCreator Creator { get; }

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IObjectCreator objectCreator)
        {
            ConfigurationProvider = objectFactoryConfigurationProvider;

            Creator = objectCreator;
            
            Interceptor = AbstractObjectFactoryInterceptor.Instance;
        }

        public TService[] Create<TTarget, TService>(TTarget instance) where TService : class
        {
            return Create<TTarget, TService>(instance, string.Empty);
        }

        public ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TService>(TTarget target) where TService : class
        {
            return ConfigurationFor<TTarget, TService>(target, string.Empty);
        }

        public TService[] Create<TTarget, TService>(TTarget target, string name) where TService : class
        {
            var list = Array.Empty<TService>();

            try
            {
                Interceptor.OnEntry(target, name);

                list = ConfigurationFor<TTarget, TService> (target, name).Select(item=> Creator.Create<TService>(item.ImplementationType)).ToArray();

                Interceptor.OnSuccess(target, name, list);
                
            }
            catch (Exception ex)
            {
                Interceptor.OnError(target, name, list, ex);

                throw;
            }
            finally
            {
                Interceptor.OnExit(target, name, list);
            }
            return list;
        }

        public ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TService>(TTarget target, string name) where TService : class
        {
            return ConfigurationProvider.Provide<TTarget, TService>(target, name);
        }
    }
}
