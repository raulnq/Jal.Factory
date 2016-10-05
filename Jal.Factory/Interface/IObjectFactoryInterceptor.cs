using System;
using System.Collections.Generic;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryInterceptor
    {
        void OnEntry<TTarget>(TTarget instance, string name);

        void OnSuccess<TTarget, TResult>(TTarget instance, string name, IList<TResult> results);

        void OnError<TTarget, TResult>(TTarget instance, string name, IList<TResult> results, Exception exception);

        void OnExit<TTarget, TResult>(TTarget instance, string name, IList<TResult> results);
    }
}
