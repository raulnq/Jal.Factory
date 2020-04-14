using Microsoft.Extensions.DependencyInjection;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{
    public class ObjectFactoryBuilder : IObjectFactoryBuilder
    {
        private readonly IServiceCollection _servicecollection;
        public ObjectFactoryBuilder(IServiceCollection servicecollection)
        {
            _servicecollection = servicecollection;
        }

        public IObjectFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _servicecollection.AddSingleton<TService, TImplementation>();

            return this;
        }

        public IObjectFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _servicecollection.AddTransient<TService, TImplementation>();

            return this;
        }

        public IObjectFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _servicecollection.AddSingleton<IObjectFactoryConfigurationSource, TImplementation>();

            return this;
        }
    }
}
