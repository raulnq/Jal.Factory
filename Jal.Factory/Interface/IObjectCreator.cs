using System;

namespace Jal.Factory
{
    public interface IObjectCreator
    {
        T Create<T>(Type type) where T:class;
    }
}