# Jal.Factory
Just another library to implement the factory method pattern

## How to use?

### Default implementation

I only suggest to use this implementation on simple apps.

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
var factory = ObjectFactory.Builder.UseLocator(locator).UseConfigurationSource(new IObjectFactoryConfigurationSource[]{ new ObjectFactoryConfigurationSource() }).Create;
```    
Use the factory
```c++
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
```
### Castle Windsor Integration [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.Installer)

The [Jal.Locator.CastleWindsor](https://www.nuget.org/packages/Jal.Locator.CastleWindsor/) and [Jal.Finder library](https://www.nuget.org/packages/Jal.Finder/) are needed.

Setup the Jal.Finder library
```c++
var directory = AppDomain.CurrentDomain.BaseDirectory;

var finder = Finder.Impl.AssemblyFinder.Builder.UsePath(directory).Create;
```
Setup the Castle Windsor container
```c++
var container = new WindsorContainer();

container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
```
Install the Jal.Locator.CastleWindsor library
```c++
container.Install(new ServiceLocatorInstaller());
```
Install the Jal.Factory library, use the FactoryInstaller class included
```c++
var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

container.Install(new FactoryInstaller(assemblies));
```
Note: The assemblies passed to the installer are where the library will find classes derived from AbstractObjectFactoryConfigurationSource.

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
Tag the assembly container of the ObjectFactoryConfigurationSource class in order to be read by the library
```c++
[assembly: AssemblyTag()]
```    
Resolve a instance of the interface IObjectFactory
```c++
var factory = container.Resolve<IObjectFactory>();
```   
Use the factory
```c++
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
``` 
### LightInject Integration [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.LightInject.Installer)

The Jal.Locator.LightInject and Jal.Finder library are needed. 

Setup the Jal.Finder library

    var directory = AppDomain.CurrentDomain.BaseDirectory;

    var finder = Finder.Impl.AssemblyFinder.Builder.UsePath(directory).Create;

Setup the LightInject container

    var container = new ServiceContainer();
    
Install the Jal.Locator.CastleWindsor library

    container.RegisterFrom<ServiceLocatorCompositionRoot>();
    
Install the Jal.Factory library, use the FactoryInstaller class included

    var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();

    container.RegisterFactory(assemblies);
    
Register your services, it's mandatory name the service with the full name of the class

    container.Register<IDoSomething, DoSomething>(typeof(DoSomething).FullName);
    
Create a class to setup the Jal.Factory library

    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
        }
    }
    
Tag the assembly container of the ObjectFactoryConfigurationSource class in order to be read by the library

    [assembly: AssemblyTag("FactorySource")]
    
Resolve a instance of the interface IObjectFactory

    var factory = container.GetInstance<IObjectFactory>();

Use the factory

    var customer = new Customer(){Age = 25};

    var services = factory.Create<Customer, IDoSomething>(customer);

[![Build status](https://ci.appveyor.com/api/projects/status/c63jmwrdr2iussdm?svg=true)](https://ci.appveyor.com/project/raulnq/jal-factory)
[![NuGet](https://img.shields.io/nuget/v/Jal.Factory.svg)](https://www.nuget.org/packages/Jal.Factory) 
