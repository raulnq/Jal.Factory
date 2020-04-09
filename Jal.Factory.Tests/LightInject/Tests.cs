using Jal.Factory.LightInject.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Jal.Factory.Tests.LightInject
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new ServiceContainer();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var factory = container.GetFactory();

            tests.Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new ServiceContainer();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var factory = container.GetFactory();

            tests.Create_WithCustomerLessThan18_ShouldBeNotEmpty(factory);
        }
    }
}
