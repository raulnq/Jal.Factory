using Jal.Factory.Interface;

namespace Jal.Factory.Fluent.Interface
{
    public interface IObjectFactoryCreateBuilder
    {
        IObjectFactory Create { get; }
    }
}