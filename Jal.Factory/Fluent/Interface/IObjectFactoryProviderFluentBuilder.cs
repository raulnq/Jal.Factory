using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryProviderFluentBuilder
    {
        IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider);

        IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);
    }
}