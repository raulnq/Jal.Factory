using System.Collections.Generic;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction> : IObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _groupName;

        public ObjectFactoryConfigurationGroupCreateBuilder(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string groupName)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _groupName = groupName;
        }

        public IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TResult>() where TResult : TRestriction
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TResult), GroupName = _groupName };

            _objectFactoryConfigurationItems.Add(value);

            return new ObjectFactoryConfigurationWhenBuilder<TTarget>(value);
        }

    }
}