using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Jal.Factory.Impl;
using Jal.Factory.Interface;

namespace Jal.Factory.Installer
{
    public class FactoryInstaller : IWindsorInstaller
    {
        private readonly Assembly[] _assemblies;

        private readonly IObjectFactoryConfigurationSource[] _sources;

        public FactoryInstaller(Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        public FactoryInstaller(IObjectFactoryConfigurationSource[] sources)
        {
            _sources = sources;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IObjectFactory)).ImplementedBy(typeof(ObjectFactory)).LifestyleSingleton(),
                Component.For(typeof(IObjectCreator)).ImplementedBy(typeof(ObjectCreator)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationProvider)).ImplementedBy(typeof(ObjectFactoryConfigurationProvider)).LifestyleSingleton()
            );

            if (_assemblies != null)
            {
                foreach (var assemblysource in _assemblies)
                {
                    var assemblyDescriptor = Classes.FromAssembly(assemblysource);
                    container.Register(assemblyDescriptor.BasedOn<AbstractObjectFactoryConfigurationSource>().WithServiceAllInterfaces());
                }
            }

            if (_sources != null)
            {
                foreach (var source in _sources)
                {
                    container.Register(Component.For(typeof(IObjectFactoryConfigurationSource)).ImplementedBy(source.GetType()).Named(source.GetType().FullName).LifestyleSingleton());
                }
            }
            
        }
    }
}