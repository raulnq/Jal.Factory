using Jal.Locator.LightInject;
using LightInject;
using System;
using System.Linq;

namespace Jal.Factory.LightInject.Installer
{

    public static class ServiceContainerExtension
    {
        public static void AddFactory(this IServiceContainer container, Action<IFactoryBuilder> action=null)
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

            if(action!=null)
            {
                action(new FactoryBuilder(container));
            }
        }

        public static IObjectFactory GetFactory(this IServiceContainer container)
        {
            return container.GetInstance<IObjectFactory>();
        }
    }
}
