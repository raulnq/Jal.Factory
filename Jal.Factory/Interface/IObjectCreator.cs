using System.Runtime.InteropServices.ComTypes;

namespace Jal.Factory.Interface
{
    public interface IObjectCreator
    {
        T Create<T>(string name) where T:class;
    }
}