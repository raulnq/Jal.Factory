namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationCreateBuilder<out TTarget, in TRestriction>
    {
        IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TResult>() where TResult : TRestriction;
    }
}