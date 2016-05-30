using System;
using Jal.Factory.Impl;
using Jal.Factory.Model;
using Jal.Factory.Tests.Attribute;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryConfigurationRuntimeProviderTests
    {
        public ObjectFactoryConfigurationRuntimePicker Create()
        {
            var fixure = new Fixture();

            return fixure.Create<ObjectFactoryConfigurationRuntimePicker>();
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithNullFilter_ShouldBeTrue(ObjectFactoryConfigurationItem item, Customer customer)
        {
            var sut = Create();

            item.Filter = null;

            var service = new DoSomething();

            var result = sut.Pick(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithNotValidFilter_ShouldBeTrue(ObjectFactoryConfigurationItem item, Customer customer)
        {
            var sut = Create();

            item.Filter = (Func<Customer, string, bool>)((t, r) => true);

            var service = new DoSomething();

            var result = sut.Pick(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithValidFilter_ShouldBeTrue(ObjectFactoryConfigurationItem item, Customer customer)
        {
            var sut = Create();

            item.Filter = (Func<Customer, IDoSomething, bool>)((t, r) => true);

            var service = new DoSomething();

            var result = sut.Pick(item, customer, service);

            result.ShouldBe(true);
        }

        [Test]
        [AutoDataBuilder]
        public void Select_WithValidFilter_ShouldBeFalse(ObjectFactoryConfigurationItem item, Customer customer)
        {
            var sut = Create();

            item.Filter = (Func<Customer, IDoSomething, bool>)((t, r) => false);

            var service = new DoSomething();

            var result = sut.Pick(item, customer, service);

            result.ShouldBe(false);
        }
    }
}
