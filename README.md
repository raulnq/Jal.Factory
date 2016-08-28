# Jal.Factory
Just another library to implement the factory method pattern

##How to use?

###Default implementation

I only suggest to use this implementation on simple apps.

Create an instance of the locator

    var locator = ServiceLocator.Builder.Create as ServiceLocator;

Register your service

    locator.Register(typeof(IDoSomething), new DoSomething(), typeof(DoSomething).FullName);

Create a class to setup the Jal.Factory library

    public class ObjectFactoryConfigurationSource : AbstractObjectFactoryConfigurationSource
    {
        public ObjectFactoryConfigurationSource()
        {
            For<Customer, IDoSomething>().Create<DoSomething>().When(x => x.Age > 18);
        }
    }
 
Create an instance of the factory

    var factory = ObjectFactory.Builder.UseServiceLocator(locator).UseConfigurationSource(new IObjectFactoryConfigurationSource[]{new ObjectFactoryConfigurationSource()}).Create;
    
Use the factory

    var customer = new Customer(){Age = 25};

    var services = factory.Create<Customer, IDoSomething>(customer);

###Castle Windsor Integration

The Jal.Locator.CastleWindsor and Jal.Finder library are needed.

Setup the Jal.Finder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;
	var finder = Finder.Impl.AssemblyFinder.Builder.UsePath(directory).Create;

Setup the Castle Windsor container

	var container = new WindsorContainer();
	container.Kernel.Resolver.AddSubResolver(new ArrayResolver(container.Kernel));
	
Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());
	
Install the Jal.Factory library, use the FactoryInstaller class included

	var assemblies = finder.GetAssembliesTagged<AssemblyTagAttribute>();
	container.Install(new FactoryInstaller(assemblies));
	
Register your services, it's mandatory name the service with the same full name of the class

	container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named(typeof(DoSomething).FullName)));
	
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

	var factory = container.Resolve<IObjectFactory>();

Use the factory

	var customer = new Customer(){Age = 25};

	var services = factory.Create<Customer, IDoSomething>(customer);
