using System;
using System.Collections.Generic;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
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

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Person), null, string.Empty)
                    }
            }
            );

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

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Person), typeof(DoSomething))
                    }
            }
            );

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), string.Empty).ShouldBeEmpty();
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow(null)]
        [DataRow("name")]
        public void Provide_WithValidTypeAndInvalidName_ShouldBeEmpty(string name)
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration() {Items= new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething))
                    }
                }
            );

            var sut = new ObjectFactoryConfigurationProvider(new []{ source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), name).ShouldBeEmpty();
        }




        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndInvalidSelector_ShouldNotBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), (Func<string, bool>)(t => true))
                    }
            });

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide<Customer, IDoSomething>(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidFalseSelector_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(Customer), (Func<Customer, bool>)(t => false))
                    }
            });

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer)));

            configuration.ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidTrueSelectorAndInvalidResultType_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(Customer), (Func<Customer, bool>)(t => true))
                    }
            });

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer)));

            configuration.ShouldBeEmpty();
        }

        [TestMethod]
        public void Provide_WithValidTypeAndValidNameAndValidTrueSelectorAndValidResultType_ShouldNotBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), (Func<Customer, bool>)(t => true))
                    }
            });

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            var configuration = sut.Provide<Customer, IDoSomething>(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof (Customer)));

            configuration.ShouldNotBeEmpty();
        }
    }
}
