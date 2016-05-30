using System;
using System.Collections.Generic;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryInterceptor
    {
        void OnEntry<TTarget>(TTarget instance, string name);

        void OnSuccess<TTarget, TResult>(TTarget instance, string name, List<TResult> results);

        void OnError<TTarget, TResult>(TTarget instance, string name, List<TResult> results, Exception exception);

        void OnExit<TTarget, TResult>(TTarget instance, string name, List<TResult> results);
    }
}
