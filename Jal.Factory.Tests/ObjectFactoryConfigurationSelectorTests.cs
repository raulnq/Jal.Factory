using System;
using Jal.Factory.Impl;
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
    public class ObjectFactoryConfigurationSelectorTests
    {
        [Test]
        [AutoDataBuilder]
        public void Select_WithNullFilter_ShouldBeTrue(ObjectFactoryConfigurationSelector sut, ObjectFactoryConfigurationItem item, Customer customer, Fixture fixture)
        {
            item.Filter = null;

            var service = fixture.Create<IDoSomething>();

            var result = sut.Select(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithNotValidFilter_ShouldBeTrue(ObjectFactoryConfigurationSelector sut, ObjectFactoryConfigurationItem item, Customer customer, Fixture fixture)
        {
            item.Filter = (Func<Customer, string, bool>)((t, r) => true);

            var service = fixture.Create<IDoSomething>();

            var result = sut.Select(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithValidFilter_ShouldBeTrue(ObjectFactoryConfigurationSelector sut, ObjectFactoryConfigurationItem item, Customer customer, Fixture fixture)
        {
            item.Filter = (Func<Customer, IDoSomething, bool>)((t, r) => true);

            var service = fixture.Create<IDoSomething>();

            var result = sut.Select(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithValidFilter_ShouldBeFalse(ObjectFactoryConfigurationSelector sut, ObjectFactoryConfigurationItem item, Customer customer, Fixture fixture)
        {
            item.Filter = (Func<Customer, IDoSomething, bool>)((t, r) => false);

            var service = fixture.Create<IDoSomething>();

            var result = sut.Select(item, customer, service);

            result.ShouldBe(false);
        }
    }
}
