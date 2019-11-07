using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests.Default
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var locator = new ServiceLocator();

            locator.Register(typeof(IDoSomething), new DoSomething(), typeof(DoSomething).FullName);

            var config = new AutoObjectFactoryConfigurationSource();

            var factory = ObjectFactory.Create(new IObjectFactoryConfigurationSource[] {config}, locator);

            var customer = new Customer() { Age = 25 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }
    }
}
