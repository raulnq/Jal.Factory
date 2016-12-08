using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryProviderBuilder
    {
        IObjectFactoryInterceptorBuilder UseConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);
    }
}