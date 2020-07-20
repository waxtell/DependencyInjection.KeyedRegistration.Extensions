using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
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
