using System;
using System.Linq;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class ObjectFactoryConfigurationProvider : IObjectFactoryConfigurationProvider
    {
        public ObjectFactoryConfiguration Configuration { get; }

        public IObjectFactoryConfigurationSource[] Sources { get; }

        public ObjectFactoryConfigurationProvider(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            if (objectFactoryConfigurationSources == null)
            {
                throw new ArgumentNullException(nameof(objectFactoryConfigurationSources));
            }

            Sources = objectFactoryConfigurationSources;

            Configuration = new ObjectFactoryConfiguration()
            {
                Items = Sources
                .Select(objectFactoryConfigurationSource => objectFactoryConfigurationSource.Source())
                .Where(source => source != null)
                .SelectMany(source => source.Items).ToList()
            };

            foreach (var objectFactoryConfigurationItem in Configuration.Items.Where(objectFactoryConfigurationItem => objectFactoryConfigurationItem.ResultType == null))
            {
                throw new ArgumentException($"The return type for the item named {objectFactoryConfigurationItem.GroupName} is null");
            }
        }

        private static bool IsSelected<TTarget>(ObjectFactoryConfigurationItem objectFactoryConfigurationItem, TTarget instance)
        {
            var selector = objectFactoryConfigurationItem.Selector as Func<TTarget, bool>;

            var isselected = true;

            if (selector != null)
            {
                isselected = selector(instance);
            }

            return isselected;
        }

        public ObjectFactoryConfigurationItem[] Provide<TTarget>(TTarget instance, string name)
        {
            return Configuration.Items
                .Where(configurationItem => 
                IsSameType<TTarget>(configurationItem) && 
                IsSameGroup(configurationItem, name) && 
                IsSelected(configurationItem, instance)
                ).ToArray();
        }

        private static bool IsSameType<TTarget>(ObjectFactoryConfigurationItem objectFactoryConfigurationItem)
        {
            return objectFactoryConfigurationItem.TargetType == typeof (TTarget);
        }

        private static bool IsSameGroup(ObjectFactoryConfigurationItem objectFactoryConfigurationItem, string groupname)
        {
            return objectFactoryConfigurationItem.GroupName == groupname;
        }
    }
}
