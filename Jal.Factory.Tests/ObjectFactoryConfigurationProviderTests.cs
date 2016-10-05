using System;
using System.Collections.Generic;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Model;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryConfigurationProviderTests
    {
        [Test]
        public void Provide_WithNonExistingName_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration() {Items= new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething))
                    }
                }
            );

            var sut = new ObjectFactoryConfigurationProvider(new []{ source.Object });

            sut.Provide(new Customer(), string.Empty).ShouldBeEmpty();
        }

        [Test]
        public void Provide_WithNoConfigurationItems_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration());

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldBeEmpty();
        }

        [Test]
        public void Provide_WithWrongSelector_ShouldNotBeEmpty()
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

            sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }

        [Test]
        public void Provide_WithNullReturnType_ShouldThrowException()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>
                    {
                        new ObjectFactoryConfigurationItem(typeof(Customer))
                    }
            });

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            Should.Throw<ArgumentException>(() => sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))));
        }

        [Test]
        public void Provide_WithValidSelector_ShouldNotBeEmpty()
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
            
            sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }
    }
}
