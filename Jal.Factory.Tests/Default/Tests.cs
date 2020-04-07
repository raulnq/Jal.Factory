using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace Jal.Factory.Tests.Default
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void Create_WithCustomerOlderThan25_ShouldBeNotEmpty()
        {
            var tests = new TestCases();

            var locator = new ServiceLocator();

            locator.Register(typeof(IDoSomething), new DoSomething(), typeof(DoSomething).FullName);

            var config = new ObjectFactoryConfigurationSource();

            var factory = new ObjectFactory (new ObjectFactoryConfigurationProvider(new IObjectFactoryConfigurationSource[] { config }), new ObjectCreator(locator));

            tests.Create_WithCustomerOlderThan25_ShouldBeNotEmpty(factory);
        }
    }
}
