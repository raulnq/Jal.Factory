using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Factory.Tests.Attribute;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryConfigurationProviderTests
    {
        [Test]
        [AutoDataBuilder]
        public void Provide_WithNonExistingName_ShouldBeEmpty(ObjectFactoryConfigurationProvider sut, Customer customer)
        {
            sut.Provide(customer, string.Empty).ShouldBeEmpty();
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithInvalidSelector_ShouldNotBeEmpty(ObjectFactoryConfigurationProvider sut, Customer customer, Fixture fixture)
        {
            sut.Configuration.Items[0].GroupName = "Default_Customer";
            sut.Configuration.Items[0].TargetType = typeof(Customer);
            sut.Configuration.Items[0].ResultType = fixture.Create<IDoSomething>().GetType();
            sut.Configuration.Items[0].Selector = (Func<string, bool>)(t => true);
            sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }

        [Test]
        [AutoDataBuilder(true)]
        public void Provide_WithNullReturnType_ShouldThrowException(ObjectFactoryConfigurationProvider sut, Customer customer)
        {
            sut.Configuration.Items[0].GroupName = "Default_Customer";
            sut.Configuration.Items[0].TargetType = typeof(Customer);
            sut.Configuration.Items[0].ResultType = null;
            Should.Throw<ArgumentException>(() => sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))));
        }

        [Test]
        [AutoDataBuilder]
        public void Provide_WithValidSelector_ShouldNotBeEmpty(ObjectFactoryConfigurationProvider sut, Customer customer, Fixture fixture)
        {
            sut.Configuration.Items[0].GroupName = "Default_Customer";
            sut.Configuration.Items[0].TargetType = typeof(Customer);
            sut.Configuration.Items[0].ResultType = fixture.Create<IDoSomething>().GetType();
            sut.Configuration.Items[0].Selector = (Func<Customer, bool>)(t => true);
            sut.Provide(customer, ObjectFactorySettings.BuildDefaultName(typeof(Customer))).ShouldNotBeEmpty();
        }


        [Test]
        [AutoDataBuilder]
        public void Provide_WithValidSelector_ShouldNotBeEmpty(Fixture result)
        {
            var sut = new ObjectFactoryConfigurationProvider(new[] { result.Create<IObjectFactoryConfigurationSource>() });
            sut.Configuration.Items.ShouldNotBeEmpty();
        }
    }
}
