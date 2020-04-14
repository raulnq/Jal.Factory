using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Jal.Factory.Installer
{
    public class FactoryBuilder : IFactoryBuilder
    {
        private readonly IWindsorContainer _container;
        public FactoryBuilder(IWindsorContainer container)
        {
            _container = container;
        }

        public IFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());

            return this;
        }

        public IFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleTransient());

            return this;
        }

        public IFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _container.Register(Component.For<IObjectFactoryConfigurationSource>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());

            return this;
        }
    }
}