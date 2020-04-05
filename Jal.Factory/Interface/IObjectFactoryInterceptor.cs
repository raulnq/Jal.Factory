using System;
using System.Collections.Generic;

namespace Jal.Factory
{
    public interface IObjectFactoryInterceptor
    {
        void OnEntry<TTarget>(TTarget target, string name);

        void OnSuccess<TTarget, TService>(TTarget target, string name, IList<TService> services);

        void OnError<TTarget, TService>(TTarget target, string name, IList<TService> services, Exception exception);

        void OnExit<TTarget, TService>(TTarget target, string name, IList<TService> services);
    }
}
