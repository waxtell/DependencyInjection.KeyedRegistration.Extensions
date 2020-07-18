namespace DependencyInjection.KeyedRegistration.Extensions.Tests
{
    public class Person : IPerson
    {
        public virtual string Name { get; }

        public Person(string name)
        {
            Name = name;
        }

        public Person() : this(null)
        {
        }
    }
}