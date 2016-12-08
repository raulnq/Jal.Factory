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
        public void Provide_WithNoConfigurationItems_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration());

            var name = $"Default_{typeof(Customer).Name}";

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide(new Customer(), name).ShouldBeEmpty();
        }

        [Test]
        public void Provide_WithInvalidType_ShouldBeEmpty()
        {
            var source = new Mock<IObjectFactoryConfigurationSource>();

            var name = $"Default_{typeof(Customer).Name}";

            source.Setup(x => x.Source()).Returns(new ObjectFactoryConfiguration()
            {
                Items = new List<ObjectFactoryConfigurationItem>()
                    {
                        new ObjectFactoryConfigurationItem(typeof(Person), typeof(DoSomething), name)
                    }
            }
            );

            var sut = new ObjectFactoryConfigurationProvider(new[] { source.Object });

            sut.Provide(new Customer(), name).ShouldBeEmpty();
        }

        [Test]
        [TestCase("")]
        [TestCase("  ")]
        [TestCase(null)]
        [TestCase("name")]
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

            sut.Provide(new Customer(), name).ShouldBeEmpty();
        }




        [Test]
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

            sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }


        [Test]
        public void Provide_WithValidTypeAndValidNameAndValidSelector_ShouldNotBeEmpty()
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

            var configuration = sut.Provide(new Customer(), ObjectFactorySettings.BuildDefaultName(typeof (Customer)));

            configuration.ShouldNotBeEmpty();
        }
    }
}
