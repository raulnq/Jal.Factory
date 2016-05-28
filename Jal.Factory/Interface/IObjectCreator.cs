using System;

namespace Jal.Factory.Interface
{
    public interface IObjectCreator
    {
        T Create<T>(Type type) where T:class;
    }
}