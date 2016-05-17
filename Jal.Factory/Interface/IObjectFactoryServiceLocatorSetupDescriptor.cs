using Jal.Factory.Fluent;
using Jal.Locator.Interface;

namespace Jal.Factory.Interface
{
    public interface IObjectFactoryServiceLocatorSetupDescriptor
    {
        IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);
    }
}