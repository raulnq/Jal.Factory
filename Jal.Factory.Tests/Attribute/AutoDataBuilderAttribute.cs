using Cignium.Framework.Infrastructure.Factory.Tests.Interfaces;
using Cignium.Framework.Infrastructure.Factory.Tests.Model;
using Jal.Factory.Interface;
using Jal.Factory.Model;
using Jal.Locator.Interface;
using Moq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.NUnit2;

namespace Jal.Factory.Tests.Attribute
{
    public class AutoDataBuilderAttribute : AutoDataAttribute
    {
        public AutoDataBuilderAttribute(ObjectFactoryResolverType resolverType = ObjectFactoryResolverType.Name, bool withoutResult=false)
            : base(new Fixture().Customize(new AutoMoqCustomization()))
        {
            var provider = Fixture.Freeze<Mock<IObjectFactoryConfigurationProvider>>();

            var service = Fixture.Freeze<Mock<IDoSomething>>();

            var item = new ObjectFactoryConfigurationItem(typeof(Customer), service.Object.GetType(), resolverType);

            if (withoutResult)
            {
                item.ResultType = null;
            }

            provider.Setup(x => x.Provide(It.IsAny<Customer>(), It.IsAny<string>())).Returns(new[] { item }).Verifiable();

            var selector = Fixture.Freeze<Mock<IObjectFactoryConfigurationSelector>>();

            selector.Setup(x => x.Select(It.IsAny<ObjectFactoryConfigurationItem>(), It.IsAny<Customer>(), It.IsAny<IDoSomething>())).Returns(true).Verifiable();

            var serviceLocator = Fixture.Freeze<Mock<IServiceLocator>>();

            serviceLocator.Setup(x => x.Resolve<IDoSomething>(It.IsAny<string>())).Returns(service.Object).Verifiable();

            var source = Fixture.Freeze<Mock<IObjectFactoryConfigurationSource>>();

            Fixture.RepeatCount = 1;

            var configuration = new ObjectFactoryConfiguration();

            configuration.Items.Add(item);

            source.Setup(x => x.Source()).Returns(configuration);
        }
    }
}
