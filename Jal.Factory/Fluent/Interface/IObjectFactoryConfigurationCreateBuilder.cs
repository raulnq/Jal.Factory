namespace Jal.Factory
{
    public interface IObjectFactoryConfigurationCreateBuilder<out TTarget, in TService>
    {
        IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TImplementation>() where TImplementation : TService;
    }
}