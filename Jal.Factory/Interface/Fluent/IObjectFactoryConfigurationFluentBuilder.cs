using Jal.Factory.Fluent;

namespace Jal.Factory.Interface.Fluent
{
    public interface IObjectFactoryConfigurationFluentBuilder<out TTarget, TRestriction>
    {
        IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction;
    }
}