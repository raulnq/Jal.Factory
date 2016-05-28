using Jal.Factory.Fluent;
using Jal.Locator.Interface;

namespace Jal.Factory.Interface
{
    public interface IObjectCreatorSetupDescriptor
    {
        IObjectFactorySetupDescriptor UseObjectCreator(IObjectCreator objectCreator);

        IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);
    }
}