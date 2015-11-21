using Cignium.Framework.Infrastructure.Factory.Tests.Interfaces;
using Cignium.Framework.Infrastructure.Factory.Tests.Model;
using Jal.Factory.Impl;
using Jal.Factory.Model;
using Jal.Factory.Tests.Attribute;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryTests
    {
        [Test]
        [AutoDataBuilder]
        public void Create_WithResolveByName_ShouldBeOne(ObjectFactory sut, Customer customer)
        {
            var implementations= sut.Create<Customer, IDoSomething>(customer);

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);
        }

        [Test]
        [AutoDataBuilder(ObjectFactoryResolverType.Type)]
        public void Create_WithResolveByType_ShouldBeOne(ObjectFactory sut, Customer customer)
        {

            var implementations = sut.Create<Customer, IDoSomething>(customer);

            implementations.ShouldNotBeNull();

            implementations.Length.ShouldBe(1);
        }
      
    }
}
