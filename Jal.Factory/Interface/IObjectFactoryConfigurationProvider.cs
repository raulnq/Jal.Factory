using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationProvider
    {
        IObjectFactoryConfigurationSource[] Sources { get; }

        ObjectFactoryConfiguration Configuration { get; }

        ObjectFactoryConfigurationItem[] Provide<TTarget,TResult>(TTarget target, string name);
    }
}
