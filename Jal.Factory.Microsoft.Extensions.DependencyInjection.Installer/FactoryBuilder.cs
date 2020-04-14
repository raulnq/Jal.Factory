using Microsoft.Extensions.DependencyInjection;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{
    public class FactoryBuilder : IFactoryBuilder
    {
        private readonly IServiceCollection _servicecollection;
        public FactoryBuilder(IServiceCollection servicecollection)
        {
            _servicecollection = servicecollection;
        }

        public IFactoryBuilder AddSingleton<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _servicecollection.AddSingleton<TService, TImplementation>();

            return this;
        }

        public IFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _servicecollection.AddTransient<TService, TImplementation>();

            return this;
        }

        public IFactoryBuilder AddSource<TImplementation>() where TImplementation : class, IObjectFactoryConfigurationSource
        {
            _servicecollection.AddSingleton<IObjectFactoryConfigurationSource, TImplementation>();

            return this;
        }
    }
}
