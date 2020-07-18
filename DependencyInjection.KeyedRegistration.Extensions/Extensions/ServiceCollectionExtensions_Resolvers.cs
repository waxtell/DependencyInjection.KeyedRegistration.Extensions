using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResolver<TKey, TService>(
            this IServiceCollection services)
            where TService : class
        {
            services
                .AddSingleton<Func<TKey, IEnumerable<TService>>>
                (
                    provider => provider.GetServices<TKey, TService>
                );

            return
                services
                    .AddSingleton<Func<TKey, TService>>
                    (
                        provider => provider.GetService<TKey, TService>
                    );
        }
    }
}
