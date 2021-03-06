﻿using System;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestClass]
    public class ObjectFactoryTests
    {
        [TestMethod]
        public void Create_With_ShouldBeOne()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer, IDoSomething>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] {new ObjectFactoryConfigurationItem(typeof (Customer), typeof(DoSomething), string.Empty) });

            var creator = new Mock<IObjectCreator>();

            creator.Setup(x => x.Create<IDoSomething>(It.IsAny<Type>())).Returns(new DoSomething());

            var sut = new ObjectFactory(provider.Object, creator.Object);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);

            implementations[0].ShouldBeOfType<DoSomething>();

            implementations[0].ShouldBeAssignableTo<IDoSomething>();
        }

        [TestMethod]
        public void Create_WithEmptyObjectFactoryConfigurationItem_ShouldBeEmpty()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer, IDoSomething>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new ObjectFactoryConfigurationItem[] { });

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.Create<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.ShouldBeEmpty();
        }

        [TestMethod]
        public void Create_ThrowException_ShouldThrowException()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer, IDoSomething>(It.IsAny<Customer>(), It.IsAny<string>())).Throws<Exception>();

            var sut = new ObjectFactory(provider.Object, null);

            Should.Throw<Exception>(()=>sut.Create<Customer, IDoSomething>(new Customer()));
        }

        [TestMethod]
        public void ConfigurationFor_With_ShouldBeOne()
        {
            var provider = new Mock<IObjectFactoryConfigurationProvider>();

            provider.Setup(x => x.Provide<Customer, IDoSomething>(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] { new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething), string.Empty) });

            var sut = new ObjectFactory(provider.Object, null);

            var implementations = sut.ConfigurationFor<Customer, IDoSomething>(new Customer());

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);
        }
    }
}
