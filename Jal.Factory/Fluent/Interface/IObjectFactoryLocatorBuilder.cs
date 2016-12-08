using Jal.Factory.Interface;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryLocatorBuilder
    {
        IObjectFactoryProviderBuilder UseLocator(IServiceLocator serviceLocator);
    }
}