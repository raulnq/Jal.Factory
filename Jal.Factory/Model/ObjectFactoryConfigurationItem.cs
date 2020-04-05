using System;
using System.Diagnostics;

namespace Jal.Factory
{
    [DebuggerDisplay("Name: {Name}, TargetType: {TargetType.Name}, ImplementationType: {ImplementationType.Name}, ServiceType: {ServiceType.Name} Selector: {Selector!=null}")]
    public class ObjectFactoryConfigurationItem
    {
        public ObjectFactoryConfigurationItem(Type target, Type implementation, Type service, string name, object selector)
        {
            Name = name;

            Selector = selector;

            ImplementationType = implementation;

            TargetType = target;

            ServiceType = service;
        }

        public ObjectFactoryConfigurationItem(Type target, Type implementation, Type service, string name)
        : this(target, implementation, service, name, null)
            {

            }

        public ObjectFactoryConfigurationItem(Type target, Type service, string name)
            : this(target, null, service, name)
        {

        }

        public string Name { get; }

        public Type TargetType { get; }

        public Type ImplementationType { get; set; }

        public Type ServiceType { get; }

        public object Selector { get; set; }
    }
}