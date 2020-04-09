using Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jal.Factory.Tests.Microsoft.Extensions.DependencyInjection
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new ServiceCollection();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var provider = container.BuildServiceProvider();

            var factory = provider.GetFactory();

            tests.Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new ServiceCollection();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var provider = container.BuildServiceProvider();

            var factory = provider.GetFactory();

            tests.Create_WithCustomerLessThan18_ShouldBeNotEmpty(factory);
        }
    }
}
