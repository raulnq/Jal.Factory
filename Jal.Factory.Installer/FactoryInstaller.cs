using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Factory.Impl;
using Jal.Factory.Interface;

namespace Jal.Factory.Installer
{
    public class FactoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IObjectFactory)).ImplementedBy(typeof(ObjectFactory)).LifestyleSingleton(),
                Component.For(typeof(IObjectCreator)).ImplementedBy(typeof(ObjectCreator)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationProvider)).ImplementedBy(typeof(ObjectFactoryConfigurationProvider)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationRuntimePicker)).ImplementedBy(typeof(ObjectFactoryConfigurationRuntimePicker)).LifestyleSingleton()
            );
            
            var assemblysources = AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("FactorySource");
            if (assemblysources != null)
            {
                foreach (var assemblysource in assemblysources)
                {
                    var assemblyDescriptor = Classes.FromAssembly(assemblysource);
                    container.Register(assemblyDescriptor.BasedOn<AbstractObjectFactoryConfigurationSource>().WithServiceAllInterfaces());
                }
            }
        }
    }
}