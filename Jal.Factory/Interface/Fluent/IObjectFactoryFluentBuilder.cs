namespace Jal.Factory.Interface.Fluent
{
    public interface IObjectFactoryFluentBuilder : IObjectFactoryEndFluentBuilder
    {
        IObjectFactoryFluentBuilder UseConfigurationRuntimePicker(IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker);

        IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider);

        IObjectFactoryFluentBuilder UseConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources);

        IObjectFactoryFluentBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor);
    }
}