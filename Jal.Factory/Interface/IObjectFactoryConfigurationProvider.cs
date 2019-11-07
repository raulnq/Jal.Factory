using Jal.Factory.Model;
using System.Collections.Generic;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationProvider
    {
        IEnumerable<IObjectFactoryConfigurationSource> Sources { get; }

        ObjectFactoryConfiguration Configuration { get; }

        ObjectFactoryConfigurationItem[] Provide<TTarget,TResult>(TTarget target, string name);
    }
}
