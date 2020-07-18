using DependencyInjection.KeyedRegistration.Extensions.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public class NamedScopedTests
    {
        [Fact]
        public void NamedInterfaceRegistration()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNamedScoped<string,IPerson>(p => new Person("George Washington"), "first");
            serviceCollection.AddNamedScoped<string,IPerson>(p => new Person("John Adams"), "second");

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<string, IPerson>("second");
            
            Assert.Equal("John Adams", instance.Name);
        }

        [Fact]
        public void NamedClassRegistration()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddNamedScoped(p => new Person("George Washington"), "first");
            serviceCollection.AddNamedScoped(p => new Person("John Adams"), "second");

            var provider = serviceCollection.BuildServiceProvider();

            var instance = provider.GetService<string, Person>("second");

            Assert.Equal("John Adams", instance.Name);
        }
    }
}
