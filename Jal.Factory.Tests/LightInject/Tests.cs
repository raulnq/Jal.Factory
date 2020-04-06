using Jal.Factory.LightInject.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Locator.LightInject;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Jal.Factory.Tests.LightInject
{
    [TestClass]
    public class Tests :AbstractTest
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.RegisterFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
                c.RegisterForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var factory = container.GetInstance<IObjectFactory>();

            Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.RegisterFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c =>
            {
                c.RegisterForFactory<IDoSomething, DoSomething>();
                c.RegisterForFactory<IDoSomething, DoSomethingLessThan18>();
            });

            var factory = container.GetInstance<IObjectFactory>();

            Create_WithCustomerLessThan18_ShouldBeNotEmpty(factory);
        }
    }
}
