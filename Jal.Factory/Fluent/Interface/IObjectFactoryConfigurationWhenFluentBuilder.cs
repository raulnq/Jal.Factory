using System;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationWhenFluentBuilder<out TTarget, TResult, out TRestriction>  where TResult : TRestriction
    {
        IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> When(Func<TTarget, TRestriction, bool> filter);

        IObjectFactoryConfigurationWhenFluentBuilder<TTarget, TResult, TRestriction> When(Func<TTarget, bool> selector);
    }
}