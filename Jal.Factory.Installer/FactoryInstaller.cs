using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;

namespace Jal.Factory.Installer
{
    public class FactoryInstaller : IWindsorInstaller
    {
        private readonly IObjectFactoryConfigurationSource[] _sources;

        private readonly Action<IWindsorContainer> _action;

        public FactoryInstaller(IObjectFactoryConfigurationSource[] sources, Action<IWindsorContainer> action = null)
        {
            _sources = sources;
            _action = action;
        }

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For(typeof(IObjectFactory)).ImplementedBy(typeof(ObjectFactory)).LifestyleSingleton(),
                Component.For(typeof(IObjectCreator)).ImplementedBy(typeof(ObjectCreator)).LifestyleSingleton(),
                Component.For(typeof(IObjectFactoryConfigurationProvider)).ImplementedBy(typeof(ObjectFactoryConfigurationProvider)).LifestyleSingleton()
            );

            if (_sources != null)
            {
                foreach (var source in _sources)
                {
                    container.Register(Component.For(typeof(IObjectFactoryConfigurationSource)).ImplementedBy(source.GetType()).Named(source.GetType().FullName).LifestyleSingleton());
                }
            }

            if (_action != null)
            {
                _action(container);
            }
        }
    }
}