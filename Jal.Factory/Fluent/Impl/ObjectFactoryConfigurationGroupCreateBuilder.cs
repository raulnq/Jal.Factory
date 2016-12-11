using System.Collections.Generic;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction> : IObjectFactoryConfigurationGroupCreateBuilder<TTarget, TRestriction>
    {
        private readonly List<ObjectFactoryConfigurationItem> _objectFactoryConfigurationItems;

        private readonly string _name;

        public ObjectFactoryConfigurationGroupCreateBuilder(List<ObjectFactoryConfigurationItem> objectFactoryConfigurationItems, string name)
        {
            _objectFactoryConfigurationItems = objectFactoryConfigurationItems;

            _name = name;
        }

        public IObjectFactoryConfigurationWhenBuilder<TTarget> Create<TResult>() where TResult : TRestriction
        {
            var value = new ObjectFactoryConfigurationItem(typeof(TTarget)) { ResultType = typeof(TResult), Name = _name };

            _objectFactoryConfigurationItems.Add(value);

            return new ObjectFactoryConfigurationWhenBuilder<TTarget>(value);
        }

    }
}