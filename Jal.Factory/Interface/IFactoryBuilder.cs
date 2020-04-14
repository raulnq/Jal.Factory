namespace Jal.Factory
{
    public interface IFactoryBuilder
    {
        IFactoryBuilder AddSingleton<TService, TImplementation>() 
            where TService : class
            where TImplementation : class, TService;

        IFactoryBuilder AddTransient<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        IFactoryBuilder AddSource<TImplementation>()
            where TImplementation : class, IObjectFactoryConfigurationSource;
    }
}