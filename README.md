# Jal.Factory
Just another library to implement the factory method pattern

##How to use?

Note: The Jal.Locator.CastleWindsor and Jal.AssemblyFinder library are needed.

Setup the Jal.AssemblyFinder library

	var directory = AppDomain.CurrentDomain.BaseDirectory;
	AssemblyFinder.Impl.AssemblyFinder.Current = new AssemblyFinder.Impl.AssemblyFinder(directory);

Setup the Castle Windsor container

	var container = new WindsorContainer();
	container.Kernel.Resolver.AddSubResolver(new ArrayResolver(_container.Kernel));
	
Install the Jal.Locator.CastleWindsor library

	container.Install(new ServiceLocatorInstaller());
	
Install the Jal.Factory library, use the FactoryInstaller class included

	container.Install(new FactoryInstaller());
	
Register your services, it's mandatory name the service with the same name of the class

	container.Register(Component.For<IDoSomething>().ImplementedBy<DoSomething>().LifestyleSingleton().Named("DoSomething"));
	
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

	var factory = _container.Resolve<IObjectFactory>();

Use the factory

	var customer = new Customer(){Age = 25};

	var services = factory.Create<Customer, IDoSomething>(customer);