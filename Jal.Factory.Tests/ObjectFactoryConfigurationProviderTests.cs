using System;
using System.Collections.Generic;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestClass]
    public class ObjectFactoryConfigurationProviderTests
    {

        [TestMethod]
        public void New_WithNullSources_ShouldThrowException()
        {
            Should.Throw<ArgumentNullException>(() => { var sut = new ObjectFactoryConfigurationProvider(null); });
        }

        [TestMethod]
        public void New_WithNullResultType_ShouldThrowException()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Person), null, null, string.Empty)
                    }));

            Should.Throw<ArgumentException>(() => { var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object }); });
        }

        [TestMethod]
        public void Provide_WithNullInstance_ShouldThrowException()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration());

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            Should.Throw<ArgumentException>(() => sut.Provide<Customer, IDoSomething>(null, string.Empty));
        }

        [TestMethod]
        public void Provide_WithNoConfigurationItems_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration());

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty).ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithInvalidType_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Person), typeof(DoSomething), typeof(IDoSomething), string.Empty, null)
                    }));

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty).ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndInvalidSelector_ShouldNotBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), typeof(IDoSomething), string.Empty, (Func<string, bool>)(t => true))
                    }));

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty).ShouldNotBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidFalseSelector_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), typeof(IDoSomething), string.Empty, (Func<Customer, bool>)(t => false))
                    }));

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty);

            configuration.ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidTrueSelectorAndInvalidResultType_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(int), typeof(IDoSomething), string.Empty, (Func<Customer, bool>)(t => true))
                    }));

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty);

            configuration.ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidTrueSelectorAndValidResultType_ShouldNotBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration(new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), typeof(IDoSomething), string.Empty, (Func<Customer, bool>)(t => true))
                    }));

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty);

            configuration.ShouldNotBeEmpty();
        }
    }
}
