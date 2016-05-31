using System;
using Jal.Factory.Fluent.Interface;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent.Impl
{
    public class ObjectFactoryFluentBuilder : IObjectFactoryStartFluentBuilder, IObjectFactoryFluentBuilder, IObjectFactoryProviderFluentBuilder
    {
        private IObjectCreator _objectCreator;

        private IObjectFactory _objectFactory;

        private IObjectFactoryInterceptor _objectFactoryInterceptor;

        private IObjectFactoryConfigurationRuntimePicker _objectFactoryConfigurationRuntimePicker;

        private IObjectFactoryConfigurationProvider _objectFactoryConfigurationProvider;

        public IObjectFactoryProviderFluentBuilder UseCreator(IObjectCreator objectCreator)
        {
            if (objectCreator == null)
            {
                throw new ArgumentNullException("objectCreator");
            }
            _objectCreator = objectCreator;
            return this;
        }

        public IObjectFactoryProviderFluentBuilder UseCreator(IServiceLocator serviceLocator)
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
            if (objectFactoryConfigurationRuntimePicker == null)
            {
                throw new ArgumentNullException("objectFactoryConfigurationRuntimePicker");
            }
            _objectFactoryConfigurationRuntimePicker = objectFactoryConfigurationRuntimePicker;
            return this;
        }

        public IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider)
        {
            if (objectFactoryConfigurationProvider == null)
            {
                throw new ArgumentNullException("objectFactoryConfigurationProvider");
            }
            _objectFactoryConfigurationProvider = objectFactoryConfigurationProvider;
            return this;
        }

        public IObjectFactoryFluentBuilder UseConfigurationProvider(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            if (objectFactoryConfigurationSources == null)
            {
                throw new ArgumentNullException("objectFactoryConfigurationSources");
            }
            _objectFactoryConfigurationProvider = new ObjectFactoryConfigurationProvider(objectFactoryConfigurationSources);
            return this;
        }

        public IObjectFactoryFluentBuilder UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor)
        {
            if (objectFactoryInterceptor == null)
            {
                throw new ArgumentNullException("objectFactoryInterceptor");
            }
            _objectFactoryInterceptor = objectFactoryInterceptor;
            return this;
        }

        public IObjectFactoryEndFluentBuilder UseObjectFactory(IObjectFactory objectFactory)
        {
            if (objectFactory == null)
            {
                throw new ArgumentNullException("objectFactory");
            }
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

                IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker = new ObjectFactoryConfigurationRuntimePicker();

                if (_objectFactoryConfigurationRuntimePicker != null)
                {
                    objectFactoryConfigurationRuntimePicker = _objectFactoryConfigurationRuntimePicker;
                }

               
                if (_objectFactoryConfigurationProvider == null)
                {
                    throw new Exception("An implementation of IObjectFactoryConfigurationProvider is needed");
                }

                var result = new ObjectFactory(_objectFactoryConfigurationProvider, _objectCreator, objectFactoryConfigurationRuntimePicker);

                if (_objectFactoryInterceptor != null)
                {
                    result.Interceptor = _objectFactoryInterceptor;
                }

                return result;
            }
        }
    }
}