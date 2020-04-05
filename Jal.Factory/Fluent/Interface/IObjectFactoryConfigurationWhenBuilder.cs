using System;

namespace Jal.Factory
{
    public interface IObjectFactoryConfigurationWhenBuilder<out TTarget>
    {
        void When(Func<TTarget, bool> selector);
    }
}