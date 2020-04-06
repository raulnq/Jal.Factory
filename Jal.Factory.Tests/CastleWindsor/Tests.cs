using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Locator.CastleWindsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jal.Factory.Tests.CastleWindsor
{

    [TestClass]
    public class Tests : AbstractTest
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
                c.RegisterForFactory<IDoSomething, DoSomethingLessThan18>();
            }));

            var factory = container.Resolve<IObjectFactory>();

            Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
                c.RegisterForFactory<IDoSomething, DoSomethingLessThan18>();
            }));

            var factory = container.Resolve<IObjectFactory>();

            Create_WithCustomerLessThan18_ShouldBeNotEmpty(factory);
        }
    }
}
