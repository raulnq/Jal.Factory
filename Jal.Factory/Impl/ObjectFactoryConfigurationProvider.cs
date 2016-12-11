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

        public ObjectFactoryConfigurationProvider(IObjectFactoryConfigurationSource[] sources)
        {
            if (sources == null)
            {
                throw new ArgumentNullException(nameof(sources));
            }

            Sources = sources;

            Configuration = new ObjectFactoryConfiguration()
            {
                Items = Sources.Select(source => source.Source()).Where(source => source != null).SelectMany(source => source.Items).ToList()
            };

            foreach (var item in Configuration.Items.Where(objectFactoryConfigurationItem => objectFactoryConfigurationItem.ResultType == null))
            {
                throw new ArgumentException($"The return type for the item named {item.Name} is null");
            }
        }

        public ObjectFactoryConfigurationItem[] Provide<TTarget, TResult>(TTarget instance, string name)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return Configuration.Items.Where(item => SameTargetTypeOf<TTarget>(item) && SameNameConfiguration(item, name) && IsSelected(item, instance) && ResultAssignableTo<TResult>(item)).ToArray();
        }

        private static bool IsSelected<TTarget>(ObjectFactoryConfigurationItem item, TTarget instance)
        {
            var selector = item.Selector as Func<TTarget, bool>;

            var isselected = true;

            if (selector != null)
            {
                isselected = selector(instance);
            }

            return isselected;
        }

        private static bool SameTargetTypeOf<TTarget>(ObjectFactoryConfigurationItem item)
        {
            return item.TargetType == typeof (TTarget);
        }

        private static bool ResultAssignableTo<TResult>(ObjectFactoryConfigurationItem item)
        {
            return typeof(TResult).IsAssignableFrom(item.ResultType);
        }

        private static bool SameNameConfiguration(ObjectFactoryConfigurationItem item, string name)
        {
            return item.Name == name;
        }
    }
}
