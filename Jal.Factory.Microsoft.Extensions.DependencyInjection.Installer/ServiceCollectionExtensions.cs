using Jal.Locator.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactory(this IServiceCollection servicecollection, Action<IFactoryBuilder> action = null)
        {
            servicecollection.AddServiceLocator();

            servicecollection.TryAddSingleton<IObjectFactory, ObjectFactory>();

            servicecollection.TryAddSingleton<IObjectCreator, ObjectCreator>();

            servicecollection.TryAddSingleton<IObjectFactoryConfigurationProvider, ObjectFactoryConfigurationProvider>();

            if (action != null)
            {
                action(new FactoryBuilder(servicecollection));
            }

            return servicecollection;
        }
    }
}
