﻿using System;
using Jal.Factory.Interface;
using Jal.Factory.LightInject.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Jal.Locator.LightInject.Installer;
using LightInject;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests.LightInject
{
    [TestClass]
    public class Tests
    {

        [TestMethod]
        public void Create_WithCustomerOlderThan25A_ShouldBeNotEmpty()
        {
            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.RegisterFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() });

            container.Register<IDoSomething, DoSomething>(typeof(DoSomething).FullName);

            var factory = container.GetInstance<IObjectFactory>();

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
            var container = new ServiceContainer();

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            container.RegisterFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() });

            container.Register<IDoSomething, DoSomething>(typeof(DoSomething).FullName);

            container.Register<IDoSomething, DoSomething2>(typeof(DoSomething2).FullName);

            var factory = container.GetInstance<IObjectFactory>();

            var customer = new Customer() { Age = 15 };

            var services = factory.Create<Customer, IDoSomething>(customer);

            services.ShouldNotBeEmpty();

            services.Length.ShouldBe(1);

            services[0].ShouldBeAssignableTo<IDoSomething>();

            services[0].ShouldBeOfType<DoSomething2>();
        }
    }
}
