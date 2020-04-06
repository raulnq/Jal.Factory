using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;

namespace Jal.Factory.Tests.Impl
{
    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
            For<Customer, IDoSomething>().Create<DoSomethingLessThan18>().When(x => x.Age < 18);
        }
    }
}
