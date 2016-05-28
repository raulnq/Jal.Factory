using System;
using System.Collections.Generic;
using Jal.Factory.Model;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryInterceptor
    {
        void OnEntry<TTarget>(TTarget instance, string name);

        void OnSuccess<TTarget, TResult>(TTarget instance, string name, ObjectFactoryConfigurationItem[] items, List<TResult> results);

        void OnError<TTarget>(TTarget instance, string name, Exception exception);

        void OnExit<TTarget, TResult>(TTarget instance, string name, List<TResult> results);
    }
}
