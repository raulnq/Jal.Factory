namespace Jal.Factory.Interface
{
    public interface IObjectFactorySetupDescriptor
    {
        IObjectFactorySetupDescriptor UseObjectFactoryConfigurationRuntimeProvider(IObjectFactoryConfigurationRuntimeProvider objectFactoryConfigurationRuntimeProvider);

        IObjectFactorySetupDescriptor UseObjectFactoryConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider);

        IObjectFactorySetupDescriptor WithObjectFactoryConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);

        IObjectFactory Create();
    }
}