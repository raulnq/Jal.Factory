using LightInject;

namespace Jal.Factory.LightInject.Installer
{
    public class ObjectFactoryBuilder : IObjectFactoryBuilder
    {
        private readonly IServiceContainer _container;
        public ObjectFactoryBuilder(IServiceContainer container)
        {
            _container = container;
        }

        public IObjectFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(typeof(TImplementation).FullName, new PerContainerLifetime());

            return this;
        }

        public IObjectFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(typeof(TImplementation).FullName);

            return this;
        }

        public IObjectFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _container.Register<IObjectFactoryConfigurationSource, TImplementation>(typeof(TImplementation).FullName, new PerContainerLifetime());

            return this;
        }
    }
}
