namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationFluentBuilder<out TTarget, TRestriction>
    {
        IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction;
    }
}