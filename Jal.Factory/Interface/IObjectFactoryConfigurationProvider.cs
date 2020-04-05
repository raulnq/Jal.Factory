using System.Collections.Generic;

namespace Jal.Factory
{
    public interface IObjectFactoryConfigurationProvider
    {
        IEnumerable<IObjectFactoryConfigurationSource> Sources { get; }

        ObjectFactoryConfiguration Configuration { get; }

        ObjectFactoryConfigurationItem[] Provide<TTarget, TService>(TTarget target, string name);
    }
}
