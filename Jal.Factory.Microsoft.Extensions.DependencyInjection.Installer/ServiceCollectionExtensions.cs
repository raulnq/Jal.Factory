using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddForFactory<TService, TImplementation>(this IServiceCollection servicecollection)
            where TService : class
            where TImplementation : class, TService
        {
            return servicecollection.AddSingleton<TService, TImplementation>();
        }

        public static IServiceCollection AddFactory(this IServiceCollection servicecollection, IObjectFactoryConfigurationSource[] sources, Action<IServiceCollection> action = null)
        {
            servicecollection.AddSingleton<IObjectFactory, ObjectFactory>();

            servicecollection.AddSingleton<IObjectCreator, ObjectCreator>();

            servicecollection.AddSingleton<IObjectFactoryConfigurationProvider, ObjectFactoryConfigurationProvider>();

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    servicecollection.AddSingleton(typeof(IObjectFactoryConfigurationSource), source.GetType());
                }
            }

            if (action != null)
            {
                action(servicecollection);
            }

            return servicecollection;
        }
    }
}
