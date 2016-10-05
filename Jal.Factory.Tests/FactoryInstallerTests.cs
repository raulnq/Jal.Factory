using System;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Jal.Factory.Impl;
using Jal.Factory.Installer;
using Jal.Factory.Interface;
using Jal.Finder.Atrribute;
using Jal.Finder.Impl;
using Jal.Locator.CastleWindsor.Installer;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class FactoryInstallerTests
    {
        [Test]
        public void Resolve_WithInstaller_ShouldBeNotNull()
        {
            var container = new WindsorContainer();

            var directory = AppDomain.CurrentDomain.BaseDirectory;

            var finder = AssemblyFinder.Builder.UsePath(directory).Create;

            container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));

            container.Install(new ServiceLocatorInstaller());

            var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

            container.Install(new FactoryInstaller(assemblies));

            var instance = container.Resolve<IObjectFactory>();

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