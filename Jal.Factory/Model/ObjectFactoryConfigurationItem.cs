using System;
using System.Diagnostics;

namespace Jal.Factory.Model
{
    [DebuggerDisplay("GroupName: {GroupName}, TargetType: {TargetType.Name}, ResultType: {ResultType.Name}, Selector: {Selector!=null}, Filter: {Filter!=null}")]
    public class ObjectFactoryConfigurationItem
    {
        public ObjectFactoryConfigurationItem(Type target, Type result, ObjectFactoryResolverType resolverType, string groupName, object selector, object filter)
        {
            GroupName = groupName;

            if (string.IsNullOrWhiteSpace(GroupName))
            {
                GroupName = ObjectFactorySettings.BuildDefaultName(target);
            }
            
            Selector = selector;

            ResultType = result;

            TargetType = target;

            ResolverType = resolverType;

            Filter = filter;
        }

        public ObjectFactoryConfigurationItem(Type target, Type result, ObjectFactoryResolverType resolverType, object selector, object filter)
            : this(target, result, resolverType, null, selector, filter)
        {

        }

        public ObjectFactoryConfigurationItem(Type target, Type result, ObjectFactoryResolverType resolverType)
            : this(target, result, resolverType, null, null)
        {

        }

        public ObjectFactoryConfigurationItem(Type target, ObjectFactoryResolverType resolverType)
            : this(target, null, resolverType)
        {

        }

        public ObjectFactoryConfigurationItem(Type target)
            : this(target, null, ObjectFactoryResolverType.Name)
        {

        }

        public string GroupName { get; set; }

        public Type TargetType { get; set; }

        public Type ResultType { get; set; }

        public object Selector { get; set; }

        public ObjectFactoryResolverType ResolverType { get; set; }

        public object Filter { get; set; }
    }
}