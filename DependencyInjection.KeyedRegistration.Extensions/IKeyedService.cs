namespace DependencyInjection.KeyedRegistration.Extensions
{
    public interface IKeyedService<out TKey>
    {
        TKey Key { get; }
        object Instance { get; }
    }
}