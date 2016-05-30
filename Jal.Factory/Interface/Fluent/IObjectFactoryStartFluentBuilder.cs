using Jal.Locator.Interface;

namespace Jal.Factory.Interface.Fluent
{
    public interface IObjectFactoryStartFluentBuilder
    {
        IObjectFactoryFluentBuilder UseCreator(IObjectCreator objectCreator);

        IObjectFactoryFluentBuilder UseServiceLocator(IServiceLocator serviceLocator);

        IObjectFactoryEndFluentBuilder UseObjectFactory(IObjectFactory objectFactory);
    }
}