using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Shouldly;

namespace Jal.Factory.Tests
{
    public class AbstractTest
    {
        protected void Create_WithCustomerOlderThan25_ShouldBeNotEmpty(IObjectFactory factory)
        {
            var customer = new Customer() { Age = 25 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }

        protected void Create_WithCustomerLessThan18_ShouldBeNotEmpty(IObjectFactory factory)
        {
            var customer = new Customer() { Age = 15 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomethingLessThan18>();
        }
    }
}
