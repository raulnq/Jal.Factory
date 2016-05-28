using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Factory.Interface.Fluent;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent
{
    public class ObjectFactorySetupDescriptor : IObjectFactoryStartSetupDescriptor, IObjectFactorySetupDescriptor
    {

        private IObjectCreator _objectCreator;

        private IObjectFactory _objectFactory;

        private IObjectFactoryInterceptor _objectFactoryInterceptor;

        private IObjectFactoryConfigurationRuntimePicker _objectFactoryConfigurationRuntimePicker;

        private IObjectFactoryConfigurationProvider _objectFactoryConfigurationProvider;

        private IObjectFactoryConfigurationSource[] _objectFactoryConfigurationSources;

        public IObjectFactorySetupDescriptor UseCreator(IObjectCreator objectCreator)
        {
            if (objectCreator == null)
            {
                throw new ArgumentNullException("objectCreator");
            }
            _objectCreator = objectCreator;
            return this;
        }

        public IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _objectCreator = new ObjectCreator(serviceLocator);
            return this;
        }

        public IObjectFactorySetupDescriptor UseConfigurationRuntimePicker(IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker)
        {
            _objectFactoryConfigurationRuntimePicker = objectFactoryConfigurationRuntimePicker;
            return this;
        }

        public IObjectFactorySetupDescriptor UseConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider)
        {
            _objectFactoryConfigurationProvider = objectFactoryConfigurationProvider;
            return this;
        }

        public IObjectFactorySetupDescriptor WithConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            _objectFactoryConfigurationSources = objectFactoryConfigurationSources;
            return this;
        }

        public IObjectFactorySetupDescriptor UseInterceptor(IObjectFactoryInterceptor objectFactoryInterceptor)
        {
            _objectFactoryInterceptor = objectFactoryInterceptor;
            return this;
        }

        public IObjectFactoryEndSetupDescriptor UseObjectFactory(IObjectFactory objectFactory)
        {
            _objectFactory = objectFactory;
            return this;
        } 

        public IObjectFactory Create()
        {
            if (_objectFactory!=null)
            {
                return _objectFactory;
            }

            IObjectFactoryInterceptor objectFactoryInterceptor = new NullObjectFactoryInterceptor();

            if (_objectFactoryInterceptor != null)
            {
                objectFactoryInterceptor = _objectFactoryInterceptor;
            }

            IObjectFactoryConfigurationRuntimePicker objectFactoryConfigurationRuntimePicker = new ObjectFactoryConfigurationRuntimePicker();

            if (_objectFactoryConfigurationRuntimePicker != null)
            {
                objectFactoryConfigurationRuntimePicker = _objectFactoryConfigurationRuntimePicker;
            }

            IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider = new ObjectFactoryConfigurationProvider(_objectFactoryConfigurationSources);

            if (_objectFactoryConfigurationProvider != null)
            {
                objectFactoryConfigurationProvider = _objectFactoryConfigurationProvider;
            }

            return new ObjectFactory(objectFactoryConfigurationProvider, _objectCreator, objectFactoryConfigurationRuntimePicker, objectFactoryInterceptor);
        }
    }
}