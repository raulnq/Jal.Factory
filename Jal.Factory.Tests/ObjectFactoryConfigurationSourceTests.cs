using System;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestClass]
    public class ObjectFactoryConfigurationSourceTests
    {
        [TestMethod]
        public void Source_WithFor_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>();

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBeNull();

            configuration.Items[0].Selector.ShouldBeNull();
        }

        [TestMethod]
        public void Source_WithNameForNoAction_ShouldEmpty()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>("Group", x => {});

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldBeEmpty();
        }

        [TestMethod]
        public void Source_WithNameForNullAction_ShouldThorwException()
        {
            var sut = new ObjectFactoryConfigurationSource();

            Should.Throw<Exception>(() => sut.For<Customer, IDoSomething>("Group", null));
        }

        [TestMethod]
        public void Source_WithNameFor_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>("Group", x=> x.Create<DoSomething>());

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();
            
            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].Name = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBeNull();
        }
        [TestMethod]
        public void Source_WithForCreate_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>().Create<DoSomething>();

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBeNull();
        }

        [TestMethod]
        public void Source_WithForCreateWhen_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            Func <Customer, bool> when =  x => x.Active;

            sut.For<Customer, IDoSomething>().Create<DoSomething>().When(when);

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBe(when);
        }

        [TestMethod]
        public void Source_WithNameForCreateWhen_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            Func<Customer, bool> when = x => x.Active;

            sut.For<Customer, IDoSomething>("Group", x => x.Create<DoSomething>().When(when));

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].Name = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBe(when);
        }
    }
}