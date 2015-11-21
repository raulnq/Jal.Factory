using System.Collections.Generic;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent
{
    public class ObjectFactoryConfigurationCreateGroupDescriptor<TTarget, TRestriction>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _groupName;

        public ObjectFactoryConfigurationCreateGroupDescriptor(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string groupName)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _groupName = groupName;
        }

        public ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction> Create<TResult>() where TResult : TRestriction
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TResult), GroupName = _groupName };

            _objectFactoryConfigurationItems.Add(value);

            return new ObjectFactoryConfigurationWhenDescriptor<TTarget, TResult, TRestriction>(value);
        }

    }
}