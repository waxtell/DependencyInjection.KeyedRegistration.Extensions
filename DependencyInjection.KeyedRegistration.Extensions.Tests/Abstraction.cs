namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public interface IAbstraction
    {
        string GetTestValue();
    }

    public class Abstraction : IAbstraction
    {
        private readonly IDependency _dependency;

        public Abstraction(IDependency dependency)
        {
            _dependency = dependency;
        }

        public string GetTestValue()
        {
            return _dependency?.ValueToTest;
        }
    }
}