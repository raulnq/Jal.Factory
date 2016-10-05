using System;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryTests
    {
        [Test]
        public void Create_With_ShouldBeOne()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] {new ObjectFactoryConfigurationItem(typeof (Customer), typeof(DoSomething)) });

            var creator = new Mock<IObjectCreator>();

            creator.Setup(x => x.Create<IDoSomething>(It.IsAny<Type>())).Returns(new DoSomething());

            var sut = new ObjectFactory(provider.Object, creator.Object);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);

            implementations[0].ShouldBeOfType<DoSomething>();

            implementations[0].ShouldBeAssignableTo<IDoSomething>();
        }

        [Test]
        public void Create_WithNoAssignableReturnType_ShouldBeEmpty()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] { new ObjectFactoryConfigurationItem(typeof(Customer), typeof(Customer)) });

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.ShouldBeEmpty();
        }

        [Test]
        public void Create_WithNoObjectFactoryConfigurationItem_ShouldBeEmpty()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new ObjectFactoryConfigurationItem[] { });

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.ShouldBeEmpty();
        }

        [Test]
        public void Create_WithNullObjectFactoryConfigurationItem_ShouldBeEmpty()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Returns((ObjectFactoryConfigurationItem[])null);

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.ShouldBeEmpty();
        }

        [Test]
        public void Create_With_ShouldThrowException()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Throws<Exception>();

            var sut = new ObjectFactory(provider.Object, null);

            Should.Throw<Exception>(()=>sut.Create<Customer, IDoSomething>(new Customer()));
        }

        [Test]
        public void ConfigurationFor_With_ShouldBeOne()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] { new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething)) {Bag = ""} });

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.ConfigurationFor<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);
        }

        [Test]
        public void Builder_With_ShouldBeOne()
        {
            var builder = ObjectFactory.Builder;

            builder.ShouldNotBeNull();

            builder.ShouldBeAssignableTo<IObjectFactoryStartFluentBuilder>();
        }
    }
}
