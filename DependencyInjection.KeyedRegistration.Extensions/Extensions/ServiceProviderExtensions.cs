using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static object GetService<TKey>(this IServiceProvider serviceProvider, Type serviceType, TKey key)
        {
            return
            (
                serviceProvider
                    .GetServices(serviceType)
                    .OfType<IKeyedService<TKey>>()
                    .Single(x => x.Key.Equals(key))
            )
            .Instance;
        }

        public static TService GetService<TKey, TService>(this IServiceProvider serviceProvider, TKey key)
        {
            return
                (TService)
                (
                    serviceProvider
                        .GetServices<TService>()
                        .OfType<IKeyedService<TKey>>()
                        .Single(x => x.Key.Equals(key))
                )
                .Instance;
        }

        public static IEnumerable<object> GetServices<TKey>(this IServiceProvider serviceProvider, Type serviceType, TKey key)
        {
            return
                serviceProvider
                    .GetServices(serviceType)
                    .OfType<IKeyedService<TKey>>()
                    .Where(x => x.Key.Equals(key))
                    .Select(x => x.Instance);
        }

        public static IEnumerable<TService> GetServices<TKey, TService>(this IServiceProvider serviceProvider, TKey key)
        {
            return
                serviceProvider
                    .GetServices<TService>()
                    .OfType<IKeyedService<TKey>>()
                    .Where(x => x.Key.Equals(key))
                    .Select(x => (TService) x.Instance);
        }
    }
}
