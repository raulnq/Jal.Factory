using System;
using System.Collections.Generic;
using Jal.Factory.Interface;

namespace Jal.Factory.Impl
{
    public abstract class AbstractObjectFactoryInterceptor : IObjectFactoryInterceptor
    {
        public static IObjectFactoryInterceptor Instance = new NullObjectFactoryInterceptor();

        public virtual void OnEntry<TTarget>(TTarget instance, string name)
        {

        }

        public virtual void OnSuccess<TTarget, TResult>(TTarget instance, string name, List<TResult> results)
        {

        }

        public virtual void OnError<TTarget, TResult>(TTarget instance, string name, List<TResult> results, Exception exception)
        {

        }

        public virtual void OnExit<TTarget, TResult>(TTarget instance, string name, List<TResult> results)
        {

        }
    }
}