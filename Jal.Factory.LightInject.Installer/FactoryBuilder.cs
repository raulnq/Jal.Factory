using LightInject;

namespace Jal.Factory.LightInject.Installer
{
    public class FactoryBuilder : IFactoryBuilder
    {
        private readonly IServiceContainer _container;
        public FactoryBuilder(IServiceContainer container)
        {
            _container = container;
        }

        public IFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(typeof(TImplementation).FullName, new PerContainerLifetime());

            return this;
        }

        public IFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(typeof(TImplementation).FullName);

            return this;
        }

        public IFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _container.Register<IObjectFactoryConfigurationSource, TImplementation>(typeof(TImplementation).FullName, new PerContainerLifetime());

            return this;
        }
    }
}
