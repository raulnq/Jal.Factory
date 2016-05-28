using System;
using Jal.Factory.Impl;
using Jal.Factory.Interface;
using Jal.Locator.Interface;

namespace Jal.Factory.Fluent
{
    public class SetupDescriptor : IServiceLocatorSetupDescriptor, IObjectFactorySetupDescriptor
    {

        private IServiceLocator _serviceLocator;

        private IObjectFactory _objectFactory;

        private IObjectFactoryConfigurationRuntimeProvider _objectFactoryConfigurationRuntimeProvider;

        private IObjectFactoryConfigurationProvider _objectFactoryConfigurationProvider;

        private IObjectFactoryConfigurationSource[] _objectFactoryConfigurationSources;

        public IObjectFactorySetupDescriptor UseServiceLocator(IServiceLocator serviceLocator)
        {
            if (serviceLocator == null)
            {
                throw new ArgumentNullException("serviceLocator");
            }
            _serviceLocator = serviceLocator;
            return this;
        }

        public IObjectFactorySetupDescriptor UseObjectFactoryConfigurationRuntimeProvider(IObjectFactoryConfigurationRuntimeProvider objectFactoryConfigurationRuntimeProvider)
        {
            _objectFactoryConfigurationRuntimeProvider = objectFactoryConfigurationRuntimeProvider;
            return this;
        }

        public IObjectFactorySetupDescriptor UseObjectFactoryConfigurationProvider(IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider)
        {
            _objectFactoryConfigurationProvider = objectFactoryConfigurationProvider;
            return this;
        }

        public IObjectFactorySetupDescriptor WithObjectFactoryConfigurationSource(IObjectFactoryConfigurationSource[] objectFactoryConfigurationSources)
        {
            _objectFactoryConfigurationSources = objectFactoryConfigurationSources;
            return this;
        }

        public IObjectFactorySetupDescriptor WithObjectFactory(IObjectFactory objectFactory)
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

            IObjectFactoryConfigurationRuntimeProvider objectFactoryConfigurationRuntimeProvider = new ObjectFactoryConfigurationRuntimeProvider();

            if (_objectFactoryConfigurationRuntimeProvider != null)
            {
                objectFactoryConfigurationRuntimeProvider = _objectFactoryConfigurationRuntimeProvider;
            }

            IObjectFactoryConfigurationProvider objectFactoryConfigurationProvider = new ObjectFactoryConfigurationProvider(_objectFactoryConfigurationSources);

            if (_objectFactoryConfigurationProvider != null)
            {
                objectFactoryConfigurationProvider = _objectFactoryConfigurationProvider;
            }

            return new ObjectFactory(objectFactoryConfigurationProvider, _serviceLocator, objectFactoryConfigurationRuntimeProvider);
        }
    }
}