using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.LightInject.Installer;
using Jal.Factory.Tests.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.LightInject.Installer;
using LightInject;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ServiceContainerExtensionTests
    {
        [Test]
        public void RegisterFactory_WithCompositionRoot_ShouldBeNotNull()
        {
            var container = new ServiceContainer();

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            container.RegisterFrom<ServiceLocatorCompositionRoot>();

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            container.RegisterFactory(assemblies);

            container.Register<IDoSomething, DoSomething>(typeof(DoSomething).FullName);

            var instance = container.GetInstance<IObjectFactory>();

            instance.ShouldNotBeNull();

            instance.ConfigurationProvider.Configuration.Items.ShouldNotBeEmpty();

            instance.ConfigurationProvider.Configuration.Items.Count.ShouldBe(1);

            instance.ConfigurationProvider.Sources.ShouldNotBeEmpty();

            instance.ConfigurationProvider.Sources.Length.ShouldBe(2);

            instance.ShouldBeAssignableTo<IObjectFactory>();

            instance.ShouldBeOfType<ObjectFactory>();
        }
    }
}