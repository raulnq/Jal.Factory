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
using Jal.Finder.Atrribute;
using Jal.Locator.CastleWindsor.Installer;
using Jal.Locator.Impl;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests.CastleWindsor
{
    [TestFixture]
    public class Tests
    {

        [Test]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var container = new WindsorContainer();

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = Finder.Impl.AssemblyFinder.Builder.UsePath(directory).Create;

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            container.Install(new FactoryInstaller(assemblies));

            container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named(typeof(DoSomething).FullName));

            var factory = container.Resolve<IObjectFactory>();

            var customer = new Customer(){Age = 25};

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }
    }
}
