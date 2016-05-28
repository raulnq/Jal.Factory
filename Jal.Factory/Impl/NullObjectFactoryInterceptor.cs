using System;
using System.Collections.Generic;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class NullObjectFactoryInterceptor : IObjectFactoryInterceptor
    {
        public void OnEntry<TTarget>(TTarget instance, string name)
        {

        }

        public void OnSuccess<TTarget, TResult>(TTarget instance, string name, ObjectFactoryConfigurationItem[] items, List<TResult> results)
        {

        }

        public void OnError<TTarget>(TTarget instance, string name, Exception exception)
        {

        }

        public void OnExit<TTarget, TResult>(TTarget instance, string name, List<TResult> results)
        {

        }
    }
}
