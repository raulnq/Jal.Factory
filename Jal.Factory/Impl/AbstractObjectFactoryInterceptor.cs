using System;
using System.Collections.Generic;

namespace Jal.Factory
{
    public abstract class AbstractObjectFactoryInterceptor : IObjectFactoryInterceptor
    {
        public static IObjectFactoryInterceptor Instance = new NullObjectFactoryInterceptor();

        public virtual void OnEntry<TTarget>(TTarget target, string name)
        {

        }

        public virtual void OnSuccess<TTarget, TService>(TTarget target, string name, IList<TService> services)
        {

        }

        public virtual void OnError<TTarget, TService>(TTarget target, string name, IList<TService> services, Exception exception)
        {

        }

        public virtual void OnExit<TTarget, TService>(TTarget target, string name, IList<TService> services)
        {

        }
    }
}