using Jal.Factory.Interface;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryStartFluentBuilder
    {
        IObjectFactoryProviderFluentBuilder UseCreator(IObjectCreator objectCreator);

        IObjectFactoryProviderFluentBuilder UseCreator(IServiceLocator serviceLocator);

        IObjectFactoryEndFluentBuilder UseObjectFactory(IObjectFactory objectFactory);
    }
}