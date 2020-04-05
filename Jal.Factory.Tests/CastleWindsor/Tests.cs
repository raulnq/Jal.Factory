using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.CastleWindsor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Jal.Factory.Tests.CastleWindsor
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Create_WithCustomerOlderThan25A_ShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() }, c=>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
            }));

            var factory = container.Resolve<IObjectFactory>();

            var customer = new Customer(){Age = 25};

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() }, c=>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
                c.RegisterForFactory<IDoSomething, DoSomething2>();
            }));

            //container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named(typeof(DoSomething).FullName));

            //container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething2>().LifestyleSingleton().Named(typeof(DoSomething2).FullName));

            var factory = container.Resolve<IObjectFactory>();

            var customer = new Customer() { Age = 15 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething2>();
        }
    }
}
