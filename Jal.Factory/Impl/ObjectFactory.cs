using System;
using System.Collections.Generic;
using Jal.Factory.Fluent.Impl;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class ObjectFactory : IObjectFactory
    {

        public static IObjectFactory Current;

        public static IObjectFactoryStartFluentBuilder Builder
        {
            get
            {
                return new ObjectFactoryFluentBuilder();
            }
        }

        public IObjectFactoryConfigurationProvider ConfigurationProvider { get; set; }

        public IObjectFactoryConfigurationRuntimePicker ConfigurationRuntimePicker { get; set; }

        public IObjectFactoryInterceptor Interceptor { get; set; }

        public IObjectCreator Creator { get; set; }

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IObjectCreator objectCreator, IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker)
        {
            ConfigurationProvider = objectFactoryConfigurationProvider;

            Creator = objectCreator;

            ConfigurationRuntimePicker = objectFactoryConfigurationRuntimePicker;

            Interceptor = AbstractObjectFactoryInterceptor.Instance;
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance) where TResult : class
        {
            var name = ObjectFactorySettings.BuildDefaultName(typeof(TTarget));

            return Create<TTarget, TResult>(instance, name);
        }

        public ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TResult>(TTarget instance) where TResult : class
        {
            var name = ObjectFactorySettings.BuildDefaultName(typeof(TTarget));

            return ConfigurationFor<TTarget, TResult>(instance, name);
        }

        public TResult[] Create<TTarget, TResult>(TTarget instance, string name) where TResult : class
        {

            var list = new List<TResult>();

            try
            {
                Interceptor.OnEntry(instance, name);

                var items = ConfigurationProvider.Provide(instance, name);

                if (items != null)
                {
                    foreach (var configurationItem in items)
                    {
                        if (typeof (TResult).IsAssignableFrom(configurationItem.ResultType))
                        {
                            var result = Creator.Create<TResult>(configurationItem.ResultType);

                            if (ConfigurationRuntimePicker.Pick(configurationItem, instance, result))
                            {
                                list.Add(result);
                            }
                        }
                    }

                    Interceptor.OnSuccess(instance, name, list);
                }
            }
            catch (Exception ex)
            {
                Interceptor.OnError(instance, name, list, ex);

                throw;
            }
            finally
            {
                Interceptor.OnExit(instance, name, list);
            }
            return list.ToArray();
        }

        public ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TResult>(TTarget instance, string name) where TResult : class
        {

            var list = new List<ObjectFactoryConfigurationItem>();

            try
            {
                var items = ConfigurationProvider.Provide(instance, name);

                if (items != null)
                {
                    foreach (var configurationItem in items)
                    {
                        if (typeof(TResult).IsAssignableFrom(configurationItem.ResultType))
                        {
                            //var result = Creator.Create<TResult>(configurationItem.ResultType);

                            //if (ConfigurationRuntimePicker.Pick(configurationItem, instance, result))
                            //{
                                list.Add(configurationItem);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
            }
            return list.ToArray();
        }
    }
}
