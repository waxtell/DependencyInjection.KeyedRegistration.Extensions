using System;
using Castle.DynamicProxy;

namespace DependencyInjection.KeyedRegistration.Extensions
{
    public sealed class NamedInstanceAdapter<TKey> : IKeyedService<TKey>, IInterceptor
    {
        public TKey Key { get; }

        public object Instance => throw new NotImplementedException();

        internal NamedInstanceAdapter(TKey key)
        {
            Key = key;
        }

        public void Intercept(IInvocation invocation)
        {
            if (IsKeyedService())
            {
                switch (invocation.Method.Name)
                {
                    case "get_Key":
                        invocation.ReturnValue = Key;
                        break;
                    case "get_Instance":
                        invocation.ReturnValue = (invocation.Proxy as IProxyTargetAccessor)?.DynProxyGetTarget();
                        break;
                }
            }
            else
            {
                invocation.Proceed();
            }

            bool IsKeyedService()
            {
                var result = invocation?.Method?.DeclaringType?.IsAssignableFrom(typeof(IKeyedService<TKey>));

                return 
                    result.HasValue && result.Value;
            }
        }
    }
}