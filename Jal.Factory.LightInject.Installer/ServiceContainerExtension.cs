using Jal.Locator.LightInject;
using LightInject;
using System;
using System.Linq;

namespace Jal.Factory.LightInject.Installer
{
    public static class ServiceContainerExtension
    {
        public static void AddForFactory<TService, TImplementation>(this IServiceContainer container) where TImplementation : TService
        {
            container.Register<TService, TImplementation>(typeof(TImplementation).FullName, new PerContainerLifetime());
        }

        public static void AddFactory(this IServiceContainer container, IObjectFactoryConfigurationSource[] sources, Action<IServiceContainer> action=null)
        {
            container.AddServiceLocator();

            if (container.AvailableServices.All(x => x.ServiceType != typeof(IObjectFactory)))
            {
                container.Register<IObjectFactory, ObjectFactory>(new PerContainerLifetime());
            }

            if (container.AvailableServices.All(x => x.ServiceType != typeof(IObjectCreator)))
            {
                container.Register<IObjectCreator, ObjectCreator>(new PerContainerLifetime());
            }

            if (container.AvailableServices.All(x => x.ServiceType != typeof(IObjectFactoryConfigurationProvider)))
            {
                container.Register<IObjectFactoryConfigurationProvider, ObjectFactoryConfigurationProvider>(new PerContainerLifetime());
            }

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    container.Register(typeof(IObjectFactoryConfigurationSource), source.GetType(), source.GetType().FullName, new PerContainerLifetime());
                }
            }

            if(action!=null)
            {
                action(container);
            }
        }
    }
}
