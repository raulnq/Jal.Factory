using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Attribute;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryTests
    {
        public ObjectFactory Create()
        {
            var fixure = new Fixture();

            fixure.Customize(new AutoMoqCustomization());

            var provider = fixure.Freeze<Mock<IObjectFactoryConfigurationProvider>>();

            var item = new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething));

            provider.Setup(x => x.Provide(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] { item }).Verifiable();

            var objectcreator = fixure.Freeze<Mock<IObjectCreator>>();

            objectcreator.Setup(x => x.Create<IDoSomething>(It.IsAny<Type>())).Returns(new DoSomething()).Verifiable();

            var source = fixure.Freeze<Mock<IObjectFactoryConfigurationSource>>();

            fixure.RepeatCount = 1;

            var configuration = new ObjectFactoryConfiguration();

            configuration.Items.Add(item);

            source.Setup(x => x.Source()).Returns(configuration);

            return fixure.Create<ObjectFactory>();
        }

        [Test]
        [AutoDataBuilder]
        public void Create_WithResolveByName_ShouldBeOne(Customer customer)
        {
            var sut = Create();

            var implementations = sut.Create<Customer, IDoSomething>(customer);

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);
        }
    }
}
