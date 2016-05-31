using System;
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
        private readonly Func<Assembly[]> _factorySourceProvider;

        public FactoryInstaller(Func<Assembly[]> factorySourceProvider)
        {
            _factorySourceProvider = factorySourceProvider;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IObjectFactory)).ImplementedBy(typeof(ObjectFactory)).LifestyleSingleton(),
                Component.For(typeof(IObjectCreator)).ImplementedBy(typeof(ObjectCreator)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationProvider)).ImplementedBy(typeof(ObjectFactoryConfigurationProvider)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationRuntimePicker)).ImplementedBy(typeof(ObjectFactoryConfigurationRuntimePicker)).LifestyleSingleton()
            );

            if (_factorySourceProvider != null)
            {
                var assemblysources = _factorySourceProvider();

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
}