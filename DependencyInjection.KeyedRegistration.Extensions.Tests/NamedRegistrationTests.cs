using DependencyInjection.KeyedRegistration.Extensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public class NamedRegistrationTests
    {
        [Fact]
        public void NamedInterfaceRegistration()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNamedSingleton(typeof(IPerson), new Person("George Washington"), "first");
            serviceCollection.AddNamedSingleton<string,IPerson>(new Person("John Adams"), "second");

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<string, IPerson>("second");
            
            Assert.Equal("John Adams", instance.Name);
        }

        [Fact]
        public void NamedClassRegistration()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNamedSingleton(typeof(Person), new Person("George Washington"), "first");
            serviceCollection.AddNamedSingleton(new Person("John Adams"), "second");

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<string, Person>("second");

            Assert.Equal("John Adams", instance.Name);
        }
    }
}
