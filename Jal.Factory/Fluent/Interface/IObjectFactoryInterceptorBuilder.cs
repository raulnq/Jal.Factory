using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryInterceptorBuilder : IObjectFactoryCreateBuilder
    {
        IObjectFactoryCreateBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor);
    }
}