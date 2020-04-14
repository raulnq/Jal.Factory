using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Locator.CastleWindsor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jal.Factory.Installer
{
    public class FactoryInstaller : IWindsorInstaller
    {
        private readonly Action<IFactoryBuilder> _action;

        public FactoryInstaller(Action<IFactoryBuilder> action = null)
        {
            _action = action;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddServiceLocator();

            if(container.Kernel.Resolver is DefaultDependencyResolver resolver)
            {
                var field = typeof(DefaultDependencyResolver).GetField("subResolvers", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

                var subResolvers = field.GetValue(resolver) as IList<ISubDependencyResolver>;

                if (subResolvers.Count == 0 || !subResolvers.OfType<CollectionResolver>().Any())
                {
                    container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
                }
            }

            if (!container.Kernel.HasComponent(typeof(IObjectFactory)))
            {
                container.Register(Component.For<IObjectFactory>().ImplementedBy<ObjectFactory>().LifestyleSingleton());
            }

            if (!container.Kernel.HasComponent(typeof(IObjectCreator)))
            {
                container.Register(Component.For<IObjectCreator>().ImplementedBy<ObjectCreator>().LifestyleSingleton());
            }

            if (!container.Kernel.HasComponent(typeof(IObjectFactoryConfigurationProvider)))
            {
                container.Register(Component.For<IObjectFactoryConfigurationProvider>().ImplementedBy<ObjectFactoryConfigurationProvider>().LifestyleSingleton());
            }

            if (_action != null)
            {
                _action(new FactoryBuilder(container));
            }
        }
    }
}