namespace Jal.Factory
{
    public interface IObjectFactoryBuilder
    {
        IObjectFactoryBuilder AddSingleton<TService, TImplementation>() 
            where TService : class
            where TImplementation : class, TService;

        IObjectFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        IObjectFactoryBuilder AddSource<TImplementation>()
            where TImplementation : class, IObjectFactoryConfigurationSource;
    }
}