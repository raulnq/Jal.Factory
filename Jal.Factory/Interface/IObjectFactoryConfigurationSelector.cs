using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationRuntimeProvider
    {
        bool Provide<TTarget, TResult>(ObjectFactoryConfigurationItem configurationItem, TTarget instance, TResult result);
    }
}
