namespace Jal.Factory
{
    public interface IObjectFactory
    {
        TService[] Create<TTarget, TService>(TTarget target, string name) where TService : class;

        TService[] Create<TTarget, TService>(TTarget target) where TService : class;

        ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TService>(TTarget target, string name) where TService : class;

        ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TService>(TTarget target) where TService : class;

        IObjectFactoryConfigurationProvider ConfigurationProvider { get; }

        IObjectCreator Creator { get; }

        IObjectFactoryInterceptor Interceptor { get; set; }
    }
}
