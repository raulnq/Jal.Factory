using System;
using System.Linq;
using Jal.Factory.Fluent.Impl;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Interface;
using Jal.Factory.Model;

namespace Jal.Factory.Impl
{
    public class ObjectFactory : IObjectFactory
    {

        public static IObjectFactory Current;

        public static IObjectFactoryLocatorBuilder Builder => new ObjectFactoryBuilder();

        public IObjectFactoryConfigurationProvider ConfigurationProvider { get; }

        public IObjectFactoryInterceptor Interceptor { get; set; }

        public IObjectCreator Creator { get; }

        public ObjectFactory(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider, IObjectCreator objectCreator)
        {
            ConfigurationProvider = objectFactoryConfigurationProvider;

            Creator = objectCreator;
            
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

            var list = new TResult[] {};

            try
            {
                Interceptor.OnEntry(instance, name);

                list = ConfigurationFor<TTarget, TResult> (instance, name).Select(configurationitem=> Creator.Create<TResult>(configurationitem.ResultType)).ToArray();

                Interceptor.OnSuccess(instance, name, list);
                
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
            return list;
        }

        public ObjectFactoryConfigurationItem[] ConfigurationFor<TTarget, TResult>(TTarget instance, string name) where TResult : class
        {
            return ConfigurationProvider.Provide(instance, name).Where(IsAssignableFrom<TResult>).ToArray();
        }

        private static bool IsAssignableFrom<TResult>(ObjectFactoryConfigurationItem objectFactoryConfigurationItem) where TResult : class
        {
            return typeof(TResult).IsAssignableFrom(objectFactoryConfigurationItem.ResultType);
        }
    }
}
