namespace Jal.Factory.Interface.Fluent
{
    public interface IObjectFactorySetupDescriptor : IObjectFactoryEndSetupDescriptor
    {
        IObjectFactorySetupDescriptor UseConfigurationRuntimePicker(IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker);

        IObjectFactorySetupDescriptor UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider);

        IObjectFactorySetupDescriptor WithConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);

        IObjectFactorySetupDescriptor UseObjectFactoryInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor);
    }
}