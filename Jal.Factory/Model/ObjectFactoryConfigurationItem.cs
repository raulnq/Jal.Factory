using System;
using System.Diagnostics;

namespace Jal.Factory.Model
{
    [DebuggerDisplay("Name: {Name}, TargetType: {TargetType.Name}, ResultType: {ResultType.Name}, Selector: {Selector!=null}")]
    public class ObjectFactoryConfigurationItem
    {
        public ObjectFactoryConfigurationItem(Type target, Type result, string name, object selector)
        {
            Name = name;

            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = ObjectFactorySettings.BuildDefaultName(target);
            }

            Selector = selector;

            ResultType = result;

            TargetType = target;
        }

        public ObjectFactoryConfigurationItem(Type target, Type result, object selector)
            : this(target, result, null, selector)
        {

        }

        public ObjectFactoryConfigurationItem(Type target, Type result)
            : this(target, result, null, null)
        {

        }

        public ObjectFactoryConfigurationItem(Type target)
            : this(target, null)
        {

        }

        public string Name { get; set; }

        public Type TargetType { get; set; }

        public Type ResultType { get; set; }

        public object Selector { get; set; }

        public dynamic Bag { get; set; }
    }
}