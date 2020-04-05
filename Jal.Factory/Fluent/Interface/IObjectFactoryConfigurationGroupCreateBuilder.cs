namespace Jal.Factory
{
    public interface IObjectFactoryConfigurationGroupCreateBuilder<out TTarget, in TService>
    {
        IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TImplementation>() where TImplementation : TService;
    }
}