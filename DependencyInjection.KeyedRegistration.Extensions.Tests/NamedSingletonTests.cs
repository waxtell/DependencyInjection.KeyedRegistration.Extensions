using DependencyInjection.KeyedRegistration.Extensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public class NamedSingletonTests
    {
        [Fact]
        public void ServiceAndImplementationGenerics_NoInstance_WithDependency()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<IDependency, Dependency>(p => new Dependency("Test Value 1"));

            serviceCollection.AddNamedSingleton<int, IAbstraction, Abstraction>(1);

            // The following two registrations are included as noise as to ensure that GetService isn't simply returning FirstOrDefault
            serviceCollection.AddNamedSingleton(typeof(IAbstraction), new Abstraction(new Dependency("Test Value 2")), 2);
            serviceCollection.AddNamedSingleton<int, IAbstraction>(p => new Abstraction(new Dependency("Test Value 3")), 3);

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<int, IAbstraction>(1);

            Assert.Equal("Test Value 1", instance.GetTestValue());
        }

        [Fact]
        public void ServiceType_Instance_DependencyProvided()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNamedSingleton<string, IAbstraction, Abstraction>("third");
            serviceCollection.AddNamedSingleton(typeof(IAbstraction), new Abstraction(new Dependency("Test Value 1")), "first");

            // The following two registrations are included as noise as to ensure that GetService isn't simply returning FirstOrDefault
            serviceCollection.AddNamedSingleton<string, IAbstraction>(p => new Abstraction(new Dependency("Test Value 2")), "second");
            serviceCollection.AddSingleton<IDependency, Dependency>(p => new Dependency("Test Value 3"));

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<string, IAbstraction>("first");

            Assert.Equal("Test Value 1", instance.GetTestValue());
        }
    }
}
