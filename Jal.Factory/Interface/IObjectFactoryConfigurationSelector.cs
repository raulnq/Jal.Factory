using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationSelector
    {
        bool Select<TTarget, TResult>(ObjectFactoryConfigurationItem configurationItem, TTarget instance, TResult result);
    }
}
