using Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Jal.Factory.Tests.Microsoft.Extensions.DependencyInjection
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Create_WithCustomerOlderThan25A_ShouldBeNotEmpty()
        {
            var container = new ServiceCollection();

            container.AddServiceLocator();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() }, c=>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
            });

            var provider = container.BuildServiceProvider();

            var factory = provider.GetService<IObjectFactory>();

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
            var container = new ServiceCollection();

            container.AddServiceLocator();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() }, c=>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomething2>();
            });

            var provider = container.BuildServiceProvider();

            var factory = provider.GetService<IObjectFactory>();

            var customer = new Customer() { Age = 15 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething2>();
        }
    }
}
