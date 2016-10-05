using System.Reflection;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using LightInject;

namespace Jal.Factory.LightInject.Installer
{
    public static class ServiceContainerExtension
    {
        public static void RegisterFactory(this IServiceContainer container, Assembly[] assemblies)
        {
            container.Register<IObjectFactory, ObjectFactory>(new PerContainerLifetime());

            container.Register<IObjectCreator, ObjectCreator>(new PerContainerLifetime());

            container.Register<IObjectFactoryConfigurationProvider, ObjectFactoryConfigurationProvider>(new PerContainerLifetime());

            var assemblysources = assemblies;

            if (assemblysources != null)
            {
                foreach (var assemblysource in assemblysources)
                {
                    foreach (var exportedType in assemblysource.ExportedTypes)
                    {
                        if (exportedType.IsSubclassOf(typeof(AbstractObjectFactoryConfigurationSource)))
                        {
                            container.Register(typeof(IObjectFactoryConfigurationSource), exportedType, exportedType.FullName, new PerContainerLifetime());
                        }
                    }
                }
            }
        }
    }
}
