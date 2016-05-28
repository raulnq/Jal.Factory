using Jal.Factory.Fluent;
using Jal.Locator.Interface;

namespace Jal.Factory.Interface
{
    public interface IServiceLocatorSetupDescriptor
    {
        IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator);
    }
}