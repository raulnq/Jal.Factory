using System;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryConfigurationWhenBuilder<out TTarget>
    {
        void When(Func<TTarget, bool> selector);
    }
}