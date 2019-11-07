using System;
using Jal.Factory.Interface;
using Jal.Factory.LightInject.Installer;
using Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.LightInject.Installer;
using Jal.Locator.Microsoft.Extensions.DependencyInjection.Extensions;
using LightInject;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests.Microsoft.Extensions.DependencyInjection
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Create_WithCustomerOlderThan25A_ShouldBeNotEmpty()
        {
            var container = new ServiceCollection();

            var namedservicecollection = container.AddServiceLocator();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() });

            namedservicecollection.AddSingleton<IDoSomething, DoSomething>(typeof(DoSomething).FullName);

            var provider = container.BuildServiceProvider();

            var factory = provider.GetService<IObjectFactory>();

            var customer = new Customer(){Age = 25};

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething>();
        }

        [TestMethod]
        public void Create_WithCustomerLessThan18_ShouldBeNotEmpty()
        {
            var container = new ServiceCollection();

            var namedservicecollection = container.AddServiceLocator();

            container.AddFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() });

            namedservicecollection.AddSingleton<IDoSomething, DoSomething>(typeof(DoSomething).FullName);

            namedservicecollection.AddSingleton<IDoSomething, DoSomething2>(typeof(DoSomething2).FullName);

            var provider = container.BuildServiceProvider();

            var factory = provider.GetService<IObjectFactory>();

            var customer = new Customer() { Age = 15 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething2>();
        }
    }
}
