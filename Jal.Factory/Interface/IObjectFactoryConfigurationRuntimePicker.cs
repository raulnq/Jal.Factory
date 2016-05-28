using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationRuntimePicker
    {
        bool Pick<TTarget, TResult>(ObjectFactoryConfigurationItem configurationItem, TTarget instance, TResult result);
    }
}
