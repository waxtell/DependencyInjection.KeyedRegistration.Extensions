using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace DependencyInjection.KeyedRegistration.Extensions
{
    public static class NamedInstanceAdapterFactory
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        internal static TInstance Create<TKey,TInstance>(TInstance instance, TKey key)
        {
            return
                (TInstance)
                Create(typeof(TInstance), instance, key);
        }

        internal static object Create<TKey>(Type instanceType, object instance, TKey key)
        {
            return
                instanceType.GetTypeInfo().IsInterface
                    ? Generator
                        .CreateInterfaceProxyWithTarget
                        (
                            instanceType,
                            new[] {typeof(IKeyedService<TKey>)},
                            instance,
                            new NamedInstanceAdapter<TKey>(key)
                        )
                    : Generator
                        .CreateClassProxyWithTarget
                        (
                            instanceType,
                            new[] {typeof(IKeyedService<TKey>)},
                            instance,
                            new NamedInstanceAdapter<TKey>(key)
                        );
        }
    }
}