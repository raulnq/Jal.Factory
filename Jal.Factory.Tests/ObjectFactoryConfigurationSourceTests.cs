using System;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    public class ObjectFactoryConfigurationSourceTests
    {
        [Test]
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

        [Test]
        public void Source_WithNamedForNoSetup_ShouldEmpty()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>("Group", x => {});

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldBeEmpty();
        }

        [Test]
        public void Source_WithNamedForNullSetup_ShouldEmpty()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>("Group", null);

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldBeEmpty();
        }

        [Test]
        public void Source_WithNameFor_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            sut.For<Customer, IDoSomething>("Group", x=> x.Create<DoSomething>());

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();
            
            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].GroupName = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBeNull();
        }
        [Test]
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

        [Test]
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

        [Test]
        public void Source_WithNamedForCreateWhen_ShouldBeOne()
        {
            var sut = new ObjectFactoryConfigurationSource();

            Func<Customer, bool> when = x => x.Active;

            sut.For<Customer, IDoSomething>("Group", x => x.Create<DoSomething>().When(when));

            var configuration = sut.Source();

            configuration.ShouldNotBeNull();

            configuration.Items.ShouldNotBeNull();

            configuration.Items.Count.ShouldBe(1);

            configuration.Items[0].GroupName = "Group";

            configuration.Items[0].TargetType.ShouldBe(typeof(Customer));

            configuration.Items[0].ResultType.ShouldBe(typeof(DoSomething));

            configuration.Items[0].Selector.ShouldBe(when);
        }
    }
}