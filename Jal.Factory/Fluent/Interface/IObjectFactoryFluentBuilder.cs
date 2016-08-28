using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryFluentBuilder : IObjectFactoryEndFluentBuilder
    {
        IObjectFactoryFluentBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor);
    }
}