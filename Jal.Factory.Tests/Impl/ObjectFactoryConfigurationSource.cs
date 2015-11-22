using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jal.Factory.Impl;
using Jal.Factory.Tests.Interfaces;
using Jal.Factory.Tests.Model;

namespace Jal.Factory.Tests.Impl
{
    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
        }
    }
}
