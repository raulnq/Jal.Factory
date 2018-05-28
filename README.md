# Jal.Factory [![Build status](https://ci.appveyor.com/api/projects/status/c63jmwrdr2iussdm?svg=true)](https://ci.appveyor.com/project/raulnq/jal-factory) [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.svg)](https://www.nuget.org/packages/Jal.Factory) [![Coverage Status](https://coveralls.io/repos/github/raulnq/Jal.Factory/badge.svg?branch=master)](https://coveralls.io/github/raulnq/Jal.Factory?branch=master)
Just another library to implement the factory method pattern

## How to use?

### Default service locator

Create an instance of the locator
```c++
var locator = new ServiceLocator();
```
Register your services, it's mandatory name the service with the full name of the class
```c++
locator.Register(typeof(IDoSomething), new DoSomething(), typeof(DoSomething).FullName);
```
Create a class to setup the Jal.Factory library
```c++
public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
{
    public ObjectFactoryConfigurationSource()
    {
        For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
    }
}
```
Create an instance of the factory
```c++
var factory = ObjectFactory.Create(new IObjectFactoryConfigurationSource[] {new ObjectFactoryConfigurationSource()}, locator);
```    
Use the factory
```c++
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
```
### Castle Windsor as service locator [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.Installer)

The [Jal.Locator.CastleWindsor](https://www.nuget.org/packages/Jal.Locator.CastleWindsor/) library is needed.

Setup the Castle Windsor container
```c++
var container = new WindsorContainer();

container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
```
Install the Jal.Locator.CastleWindsor library
```c++
container.Install(new ServiceLocatorInstaller());
```
Install the Jal.Factory library
```c++
container.Install(new FactoryInstaller(new IObjectFactoryConfigurationSource[]{ new ObjectFactoryConfigurationSource() } ));
```
Register your services, it's mandatory name the service with the full name of the class
```c++
container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named(typeof(DoSomething).FullName)));
```
Create a class to setup the Jal.Factory library
```c++
public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
{
    public ObjectFactoryConfigurationSource()
    {
        For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
    }
}
```     
Resolve an instance of IObjectFactory
```c++
var factory = container.Resolve<IObjectFactory>();
```   
Use the factory
```c++
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
``` 
### LightInject as service locator [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.LightInject.Installer)

The [Jal.Locator.LightInject](https://www.nuget.org/packages/Jal.Locator.LightInject/) library is needed. 

Setup the LightInject container
```c++
var container = new ServiceContainer();
```     
Install the Jal.Locator.CastleWindsor library
```c++
container.RegisterFrom<ServiceLocatorCompositionRoot>();
```     
Install the Jal.Factory library, use the FactoryInstaller class included
```c++
container.RegisterFactory(new IObjectFactoryConfigurationSource[] { new AutoObjectFactoryConfigurationSource() });
```    
Register your services, it's mandatory name the service with the full name of the class
```c++
container.Register<IDoSomething, DoSomething>(typeof(DoSomething).FullName);
```    
Create a class to setup the Jal.Factory library
```c++
public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
{
    public ObjectFactoryConfigurationSource()
    {
        For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
    }
}
```  
Resolve a instance of IObjectFactory
```c++
var factory = container.GetInstance<IObjectFactory>();
``` 
Use the factory
```c++
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
``` 