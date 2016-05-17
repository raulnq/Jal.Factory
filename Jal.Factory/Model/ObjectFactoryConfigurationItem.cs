using System;
using System.Diagnostics;

namespace Jal.Factory.Model
{
    [DebuggerDisplay("GroupName: {GroupName}, TargetType: {TargetType.Name}, ResultType: {ResultType.Name}, Selector: {Selector!=null}, Filter: {Filter!=null}")]
    public class ObjectFactoryConfigurationItem
    {
        public ObjectFactoryConfigurationItem(Type target, Type result, string groupName, object selector, object filter)
        {
            GroupName = groupName;

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                GroupName = ObjectFactorySettings.BuildDefaultName(target);
            }

            Selector = selector;

            ResultType = result;

            TargetType = target;

            Filter = filter;
        }

        public ObjectFactoryConfigurationItem(Type target, Type result, object selector, object filter)
            : this(target, result, null, selector, filter)
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

        public string GroupName { get; set; }

        public Type TargetType { get; set; }

        public Type ResultType { get; set; }

        public object Selector { get; set; }

        public object Filter { get; set; }

        public dynamic Bag { get; set; }
    }
}