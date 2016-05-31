namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationGroupFluentBuilder<out TTarget, TRestriction>
    {
        IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction;
    }
}