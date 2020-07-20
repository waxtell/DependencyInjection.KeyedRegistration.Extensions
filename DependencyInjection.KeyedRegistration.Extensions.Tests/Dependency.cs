namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public interface IDependency
    {
        string ValueToTest { get; }
    }

    public class Dependency : IDependency
    {
        public string ValueToTest { get;  }

        public Dependency(string valueToTest)
        {
            ValueToTest = valueToTest;
        }
    }
}
