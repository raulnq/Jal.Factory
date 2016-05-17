namespace Jal.Factory.Interface
{
    public interface IObjectFactorySetupDescriptor
    {
        IObjectFactorySetupDescriptor UseObjectFactoryConfigurationSelector(IObjectFactoryConfigurationSelector objectFactoryConfigurationSelector);

        IObjectFactorySetupDescriptor UseObjectFactoryConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider);

        IObjectFactorySetupDescriptor WithObjectFactoryConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);

        IObjectFactory Create();
    }
}