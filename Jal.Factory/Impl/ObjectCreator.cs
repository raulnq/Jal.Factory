using System;
using Jal.Locator;

namespace Jal.Factory
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
