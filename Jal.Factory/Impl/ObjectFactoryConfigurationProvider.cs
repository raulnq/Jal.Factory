﻿using System;
using System.Collections.Generic;
using System.Linq;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class ObjectFactoryConfigurationProvider : IObjectFactoryConfigurationProvider
    {
        public ObjectFactoryConfiguration Configuration { get; set; }

        public IObjectFactoryConfigurationSource[] Sources { get; set; }

        public ObjectFactoryConfigurationProvider(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            Sources = objectFactoryConfigurationSources;

            Configuration = new ObjectFactoryConfiguration();

            if (objectFactoryConfigurationSources!=null)
            {
                foreach (var objectFactoryConfigurationSource in Sources)
                {
                    Configuration.Items.AddRange(objectFactoryConfigurationSource.Source().Items);
                }
            }
        }

        public ObjectFactoryConfigurationItem[] Provide<TTarget>(TTarget instance, string name)
        {
            var objectFactoryConfigurationItems = Configuration.Items;

            var items = objectFactoryConfigurationItems.Where(x => x.TargetType == typeof(TTarget)).ToList();

            var group = items.Where(x => x.GroupName == name);

            var list = new List<ObjectFactoryConfigurationItem>();

            foreach (var configurationItem in group)
            {
                var selector = configurationItem.Selector as Func<TTarget, bool>;

                var selected = true;

                if (selector != null)
                {
                    selected = selector(instance);
                }

                if (selected)
                {
                    var type = configurationItem.ResultType;

                    if (type == null) 
                        throw new ArgumentException(string.Format("The return type for the item named {0} is null", name));

                    list.Add(configurationItem);
                }
            }

            return list.ToArray();
        }
    }
}
