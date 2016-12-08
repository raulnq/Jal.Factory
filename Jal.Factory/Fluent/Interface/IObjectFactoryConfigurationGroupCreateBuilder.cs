namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationGroupCreateBuilder<out TTarget, in TRestriction>
    {
        IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TResult>() where TResult : TRestriction;
    }
}