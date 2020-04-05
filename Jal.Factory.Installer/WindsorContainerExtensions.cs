using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace Jal.Factory.Installer
{
    public static class WindsorContainerExtensions
    {
        public static void RegisterForFactory<TService, TImplementation>(this IWindsorContainer container) 
            where TImplementation : TService
            where TService : class
        {
            container.Register(Component.For<TService>().ImplementedBy<TImplementation>().Named(typeof(TImplementation).FullName).LifestyleSingleton());
        }
    }
}