using System;
using Jal.Factory.Interface;
using Jal.Locator.Interface;

namespace Jal.Factory.Impl
{
    public class ObjectCreator : IObjectCreator
    {
        private readonly IServiceLocator _serviceLocator;

        public ObjectCreator(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public T Create<T>(Type type) where T : class
        {
            return _serviceLocator.Resolve<T>(type.FullName);
        }
    }
}
