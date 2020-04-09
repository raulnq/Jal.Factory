using Microsoft.Extensions.DependencyInjection;
using System;

namespace Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer
{
    public static class ServiceProviderExtensions
    {
        public static IObjectFactory GetFactory(this IServiceProvider provider)
        {
            return provider.GetService<IObjectFactory>();
        }
    }
}
