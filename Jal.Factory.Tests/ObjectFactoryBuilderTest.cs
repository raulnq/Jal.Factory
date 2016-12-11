using System;
using Jal.Factory.Fluent.Impl;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Interface;
using Jal.Locator.Interface;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace Jal.Factory.Tests
{
    [TestFixture]
    public class ObjectFactoryBuilderTest
    {
        [Test]
        public void UseLocator_WithNullLocator_ShouldThrowException()
        {
            var sut = new ObjectFactoryBuilder();

            Should.Throw<ArgumentNullException>(()=> sut.UseLocator(null));
        }

        [Test]
        public void UseLocator_WithNotNullLocator_ShouldBeNotNull()
        {
            var sut = new ObjectFactoryBuilder();

            var chain = sut.UseLocator(new Mock<IServiceLocator>().Object);

            chain.ShouldBeAssignableTo<IObjectFactoryProviderBuilder>();

            chain.ShouldNotBeNull();

            sut.ObjectCreator.ShouldNotBeNull();
        }

        [Test]
        public void UseConfigurationSource_WithNotNullConfigurationSource_ShouldBeNotNull()
        {
            var sut = new ObjectFactoryBuilder();

            var chain = sut.UseConfigurationSource(new [] {new Mock<IObjectFactoryConfigurationSource>().Object });

            chain.ShouldBeAssignableTo<IObjectFactoryInterceptorBuilder>();

            chain.ShouldNotBeNull();

            sut.ObjectFactoryConfigurationProvider.ShouldNotBeNull();
        }

        [Test]
        public void UseConfigurationSource_WithNullConfigurationSource_ShouldThrowException()
        {
            var sut = new ObjectFactoryBuilder();

            Should.Throw<ArgumentNullException>(() => sut.UseConfigurationSource(null));
        }

        [Test]
        public void UseInterceptor_WithNotNullInterceptor_ShouldBeNotNull()
        {
            var sut = new ObjectFactoryBuilder();

            var chain = sut.UseInterceptor(new Mock<IObjectFactoryInterceptor>().Object);

            chain.ShouldBeAssignableTo<IObjectFactoryInterceptorBuilder>();

            chain.ShouldNotBeNull();

            sut.ObjectFactoryInterceptor.ShouldNotBeNull();
        }

        [Test]
        public void UseInterceptor_WithNullInterceptor_ShouldThrowException()
        {
            var sut = new ObjectFactoryBuilder();

            Should.Throw<ArgumentNullException>(() => sut.UseInterceptor(null));
        }

        [Test]
        public void Create_WithNullConfigurationProviderAndCreator_ShouldThrowException()
        {
            var sut = new ObjectFactoryBuilder();

            Should.Throw<ApplicationException>(() => { var instance = sut.Create; } );
        }

        [Test]
        public void Create_WithConfigurationProviderAndCreator_ShouldBeNotNull()
        {
            var sut = new ObjectFactoryBuilder();

            var instance = sut.UseLocator(new Mock<IServiceLocator>().Object).UseConfigurationSource(new[] { new Mock<IObjectFactoryConfigurationSource>().Object }).UseInterceptor(new Mock<IObjectFactoryInterceptor>().Object).Create;

            instance.ShouldNotBeNull();

            instance.ShouldBeAssignableTo<IObjectFactory>();

            instance.ConfigurationProvider.ShouldNotBeNull();

            instance.Creator.ShouldNotBeNull();

            instance.Interceptor.ShouldNotBeNull();
        }
    }
}