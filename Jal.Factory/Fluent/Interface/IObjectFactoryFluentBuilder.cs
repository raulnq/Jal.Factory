using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryFluentBuilder : IObjectFactoryEndFluentBuilder
    {
        IObjectFactoryFluentBuilder UseConfigurationRuntimePicker(IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker);

        IObjectFactoryFluentBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor);
    }
}