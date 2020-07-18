using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddResolver<TKey, TService>(
            this IServiceCollection services)
            where TService : class
        {
            return
                services
                    .AddSingleton<Func<TKey, TService>>
                    (
                        provider => provider.GetService<TKey, TService>
                    );
        }
    }
}
