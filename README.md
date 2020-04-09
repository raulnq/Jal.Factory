# Jal.Factory [![Build status](https://ci.appveyor.com/api/projects/status/c63jmwrdr2iussdm?svg=true)](https://ci.appveyor.com/project/raulnq/jal-factory) [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.svg)](https://www.nuget.org/packages/Jal.Factory) [![Coverage Status](https://coveralls.io/repos/github/raulnq/Jal.Factory/badge.svg?branch=master)](https://coveralls.io/github/raulnq/Jal.Factory?branch=master)
Just another library to implement the factory method pattern

## How to use?
Create your classes
```csharp
public class Customer
{
    public string Name { get; set; }

    public int Age { get; set; }
}

public interface IDoSomething
{
    bool Apply();
}

public class DoSomething : IDoSomething
{
    public bool Apply()
    {
        return true;
    }
}

public class DoSomethingLessThan18 : IDoSomething
{
    public bool Apply()
    {
        return true;
    }
}
```
Create a class to setup the factory
```csharp
public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
{
    public ObjectFactoryConfigurationSource()
    {
        For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age >= 18);
        For<Customer, IDoSomething>().Create<DoSomethingLessThan18>().When(x => x.Age < 18);
    }
}
```
Use the factory
```csharp
var customer = new Customer(){Age = 25};

var services = factory.Create<Customer, IDoSomething>(customer);
```

## IObjectFactory interface building

### Castle Windsor [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.Installer)

```csharp
var container = new WindsorContainer();

container.AddFactory(new FactoryInstaller(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
{
    c.AddForFactory<IDoSomething, DoSomething>();
    c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
}));

var factory = container.GetFactory();
```

### LightInject [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.LightInject.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.LightInject.Installer)

```csharp
var container = new ServiceContainer();

container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
{
    c.AddForFactory<IDoSomething, DoSomething>();
    c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
});

var factory = container.GetFactory();
``` 

### Microsoft.Extensions.DependencyInjection [![NuGet](https://img.shields.io/nuget/v/Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer.svg)](https://www.nuget.org/packages/Jal.Factory.Microsoft.Extensions.DependencyInjection.Installer)

```csharp
var container = new ServiceCollection();

container.AddFactory(new IObjectFactoryConfigurationSource[] { new ObjectFactoryConfigurationSource() }, c=>
{
    c.AddForFactory<IDoSomething, DoSomething>();
    c.AddForFactory<IDoSomething, DoSomethingLessThan18>();
});

var provider = container.BuildServiceProvider();

var factory = provider.GetFactory();
``` 