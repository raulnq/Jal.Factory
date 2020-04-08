using Castle.Windsor;
using Jal.Factory.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jal.Factory.Tests.CastleWindsor
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new WindsorContainer();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var factory = container.Resolve<IObjectFactory>();

            tests.Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var container = new WindsorContainer();

            container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.AddForFactory<IDoSomething, DoSomething>();
                c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
            }));

            var factory = container.Resolve<IObjectFactory>();

            tests.Create_WithCustomerLessThan18_ShouldBeNotEmpty(factory);
        }
    }
}
