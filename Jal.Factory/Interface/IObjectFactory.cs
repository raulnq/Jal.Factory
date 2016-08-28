using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactory
    {
        TResult[] Create<TTarget, TResult>(TTarget target, string name) where TResult : class;

        TResult[] Create<TTarget, TResult>(TTarget target) where TResult : class;

        ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TResult>(TTarget target, string name) where TResult : class;

        ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TResult>(TTarget target) where TResult : class;

        IObjectFactoryConfigurationProvider ConfigurationProvider { get; }

        IObjectCreator Creator { get; }

        IObjectFactoryInterceptor Interceptor { get; set; }
    }
}
