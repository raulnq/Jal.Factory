using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Interface.Fluent;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent
{
    public class ObjectFactoryFluentBuilder : IObjectFactoryStartFluentBuilder, IObjectFactoryFluentBuilder
    {

        private IObjectCreator _objectCreator;

        private IObjectFactory _objectFactory;

        private IObjectFactoryInterceptor _objectFactoryInterceptor;

        private IObjectFactoryConfigurationRuntimePicker _objectFactoryConfigurationRuntimePicker;

        private IObjectFactoryConfigurationProvider _objectFactoryConfigurationProvider;

        private IObjectFactoryConfigurationSource[] _objectFactoryConfigurationSources;

        public IObjectFactoryFluentBuilder UseCreator(IObjectCreator objectCreator)
        {
            if (objectCreator == null)
            {
                throw new ArgumentNullException("objectCreator");
            }
            _objectCreator = objectCreator;
            return this;
        }

        public IObjectFactoryFluentBuilder UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _objectCreator = new ObjectCreator(serviceLocator);
            return this;
        }

        public IObjectFactoryFluentBuilder UseConfigurationRuntimePicker(IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker)
        {
            _objectFactoryConfigurationRuntimePicker = objectFactoryConfigurationRuntimePicker;
            return this;
        }

        public IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider)
        {
            _objectFactoryConfigurationProvider = objectFactoryConfigurationProvider;
            return this;
        }

        public IObjectFactoryFluentBuilder UseConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            if (objectFactoryConfigurationSources == null)
            {
                throw new ArgumentNullException("objectFactoryConfigurationSources");
            }
            _objectFactoryConfigurationSources = objectFactoryConfigurationSources;
            return this;
        }

        public IObjectFactoryFluentBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor)
        {
            _objectFactoryInterceptor = objectFactoryInterceptor;
            return this;
        }

        public IObjectFactoryEndFluentBuilder UseObjectFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
            return this;
        } 

        public IObjectFactory Create
        {
            get
            {
                if (_objectFactory != null)
                {
                    return _objectFactory;
                }

                IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker =
                    new ObjectFactoryConfigurationRuntimePicker();

                if (_objectFactoryConfigurationRuntimePicker != null)
                {
                    objectFactoryConfigurationRuntimePicker = _objectFactoryConfigurationRuntimePicker;
                }

                if (_objectFactoryConfigurationSources==null)
                {
                    throw new Exception("A ObjectFactoryConfigurationSource is needed");
                }

                IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider =
                    new ObjectFactoryConfigurationProvider(_objectFactoryConfigurationSources);

                if (_objectFactoryConfigurationProvider != null)
                {
                    objectFactoryConfigurationProvider = _objectFactoryConfigurationProvider;
                }

                var result =  new ObjectFactory(objectFactoryConfigurationProvider, _objectCreator,
                    objectFactoryConfigurationRuntimePicker);

                IObjectFactoryInterceptor objectFactoryInterceptor = new NullObjectFactoryInterceptor();

                if (_objectFactoryInterceptor != null)
                {
                    objectFactoryInterceptor = _objectFactoryInterceptor;
                }

                result.Interceptor = objectFactoryInterceptor;

                return result;
            }
        }
    }
}