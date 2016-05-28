using Jal.Locator.Interface;

namespace Jal.Factory.Interface.Fluent
{
    public interface IObjectFactoryStartSetupDescriptor
    {
        IObjectFactorySetupDescriptor UseCreator(IObjectCreator objectCreator);

        IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);

        IObjectFactoryEndSetupDescriptor UseObjectFactory(IObjectFactory objectFactory);
    }
}