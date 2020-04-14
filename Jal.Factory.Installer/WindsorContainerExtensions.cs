using Castle.Windsor;
using System;

namespace Jal.Factory.Installer
{
    public static class WindsorContainerExtensions
    {
        public static void AddFactory(this IWindsorContainer container, Action<IFactoryBuilder> action = null)
        {
            container.Install(new FactoryInstaller(action));
        }

        public static IObjectFactory GetFactory(this IWindsorContainer container)
        {
            return container.Resolve<IObjectFactory>();
        }
    }
}