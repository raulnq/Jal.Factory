using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFactory(this IServiceCollection servicecollection, IObjectFactoryConfigurationSource[] sources)
        {
            servicecollection.AddSingleton<IObjectFactory, Impl.ObjectFactory>();

            servicecollection.AddSingleton<IObjectCreator, ObjectCreator>();

            servicecollection.AddSingleton<IObjectFactoryConfigurationProvider, ObjectFactoryConfigurationProvider>();

            if (sources != null)
            {
                foreach (var source in sources)
                {
                    servicecollection.AddSingleton(typeof(IObjectFactoryConfigurationSource), source.GetType());
                }
            }

            return servicecollection;
        }
    }
}
