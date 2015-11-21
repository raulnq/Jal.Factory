using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryConfigurationProvider
    {
        ObjectFactoryConfigurationItem[] Provide<TTarget>(TTarget target, string name);
    }
}
