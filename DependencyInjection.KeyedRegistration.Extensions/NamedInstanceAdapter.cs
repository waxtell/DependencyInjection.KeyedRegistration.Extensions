﻿using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace DependencyInjection.KeyedRegistration.Extensions
{
    public class NamedInstanceAdapter<TKey> : IKeyedService<TKey>, IInterceptor
    {
        public TKey Key { get; }
        public object Instance { get; }

        public NamedInstanceAdapter(TKey key, object instance)
        {
            Key = key;
            Instance = instance;
        }

        public void Intercept(IInvocation invocation)
        {
            bool IsKeyedService()
            {
                var result = invocation?.Method?.DeclaringType?.IsAssignableFrom(typeof(IKeyedService<TKey>));

                return result.HasValue && result.Value;
            }
            
            if (IsKeyedService())
            {
                switch (invocation.Method.Name)
                {
                    case "get_Key":
                        invocation.ReturnValue = Key;
                        break;
                    case "get_Instance":
                        invocation.ReturnValue = Instance;
                        break;
                }
            }
            else
            {
                invocation.Proceed();
            }
        }

        public static TInstance PeelAndWrap<TInstance>(Func<TInstance, TInstance> wrap, TInstance baseInstance) 
        {
            var keyedInstance = (IKeyedService<TKey>) baseInstance;

            return
                Create
                (
                    wrap.Invoke((TInstance) keyedInstance.Instance),
                    keyedInstance.Key
                );
        }

        internal static TInstance Create<TInstance>(TInstance instance, TKey key)
        {
            return
                (TInstance)
                    Create(typeof(TInstance), instance, key);
        }

        internal static object Create(Type instanceType, object instance, TKey key)
        {
            return
                instanceType.GetTypeInfo().IsInterface
                    ? new ProxyGenerator()
                        .CreateInterfaceProxyWithTarget
                        (
                            instanceType,
                            new[] {typeof(IKeyedService<TKey>)},
                            instance,
                            new NamedInstanceAdapter<TKey>(key, instance)
                        )
                    : new ProxyGenerator()
                        .CreateClassProxyWithTarget
                        (
                            instanceType,
                            new[] {typeof(IKeyedService<TKey>)},
                            instance,
                            new NamedInstanceAdapter<TKey>(key, instance)
                        );
        }
    }
}