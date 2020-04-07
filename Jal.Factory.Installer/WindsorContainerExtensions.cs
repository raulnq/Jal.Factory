using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System;

namespace Jal.Factory.Installer
{
    public static class WindsorContainerExtensions
    {
        public static void AddForFactory<TService, TImplementation>(this IWindsorContainer container) 
            where TImplementation : TService
            where TService : class
        {
            container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());
        }

        public static void AddFactory(this IWindsorContainer container, IObjectFactoryConfigurationSource[] sources, Action<IWindsorContainer> action = null)
        {
            container.Install(new FactoryInstaller(sources, action));
        }
    }
}