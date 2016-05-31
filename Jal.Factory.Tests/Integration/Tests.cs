using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Impl;
using Jal.Factory.Installer;
using Jal.Factory.Interface;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Locator.Impl;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests.Integration
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void CreateCastleWindsor_WithCustomerOlderThan25_ServicesShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            AssemblyFinder.Impl.AssemblyFinder.Current = AssemblyFinder.Impl.AssemblyFinder.Builder.UsePath(directory).Create;

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            container.Install(new FactoryInstaller( () => AssemblyFinder.Impl.AssemblyFinder.Current.GetAssemblies("FactorySource") ));

            container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named(typeof(DoSomething).FullName));

            var factory = container.Resolve<IObjectFactory>();

            var customer = new Customer(){Age = 25};

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }

        [Test]
        public void Create_WithCustomerOlderThan25_ServicesShouldBeNotEmpty()
        {
            var locator = ServiceLocator.Builder.Create as ServiceLocator;

            locator.Register(typeof(IDoSomething), new DoSomething(), typeof(DoSomething).FullName);

            var factory = ObjectFactory.Builder.UseServiceLocator(locator).UseConfigurationSource(new IObjectFactoryConfigurationSource[]{new ObjectFactoryConfigurationSource()}).Create;

            var customer = new Customer() { Age = 25 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }
    }
}
