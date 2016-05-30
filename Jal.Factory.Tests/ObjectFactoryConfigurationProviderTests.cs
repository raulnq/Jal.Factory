using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Attribute;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Model;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryConfigurationProviderTests
    {
        public ObjectFactoryConfigurationProvider Create()
        {
            var fixure = new Fixture();

            fixure.Customize(new AutoMoqCustomization());

            var item = new ObjectFactoryConfigurationItem(typeof(Customer), typeof(DoSomething));

            var source = fixure.Freeze<Mock<IObjectFactoryConfigurationSource>>();

            fixure.RepeatCount = 1;

            var configuration = new ObjectFactoryConfiguration();

            configuration.Items.Add(item);

            source.Setup(x => x.Source()).Returns(configuration);

            var sut = fixure.Build<ObjectFactoryConfigurationProvider>().Without(x=>x.Configuration).Create();

            return sut;
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithNonExistingName_ShouldBeEmpty(Customer customer)
        {
            var sut = Create();

            sut.Provide(customer, string.Empty).ShouldBeEmpty();
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithInvalidSelector_ShouldNotBeEmpty(Customer customer)
        {
            var sut = Create();
            sut.Configuration.Items[0].Selector = (Func<string, bool>)(t => true);
            sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithNullReturnType_ShouldThrowException(Customer customer)
        {
            var sut = Create();
            sut.Configuration.Items[0].ResultType = null;
            Should.Throw<ArgumentException>(() => sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))));
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithValidSelector_ShouldNotBeEmpty(Customer customer)
        {
            var sut = Create();
            sut.Configuration.Items[0].Selector = (Func<Customer, bool>)(t => true);
            sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }


        [Test]
        [AutoDataBuilder]
        public void Provide_WithValidSelector_ShouldNotBeEmpty()
        {
            var sut = Create();
            sut.Configuration.Items.ShouldNotBeEmpty();
        }
    }
}
