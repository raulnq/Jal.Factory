using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Jal.Factory.Installer
{
    public class ObjectFactoryBuilder : IObjectFactoryBuilder
    {
        private readonly IWindsorContainer _container;
        public ObjectFactoryBuilder(IWindsorContainer container)
        {
            _container = container;
        }

        public IObjectFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());

            return this;
        }

        public IObjectFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleTransient());

            return this;
        }

        public IObjectFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _container.Register(Component.For<IObjectFactoryConfigurationSource>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());

            return this;
        }
    }
}