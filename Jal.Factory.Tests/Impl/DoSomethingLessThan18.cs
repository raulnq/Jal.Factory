using Jal.Factory.Tests.Interfaces;

namespace Jal.Factory.Tests.Impl
{
    public class DoSomethingLessThan18 : IDoSomething
    {
        public bool Apply()
        {
            return true;
        }
    }
}