using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a scoped service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddNamedScoped<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            object AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddScoped
                    (
                        serviceType,
                        AdaptedFactory
                    );
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddNamedScoped<TKey, TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            TKey key)
            where TService : class
        {
            TService AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddScoped
                    (
                        typeof(TService),
                        AdaptedFactory
                    );
        }

        /// <summary>
        /// Adds a scoped service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>/// 
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Scoped"/>
        public static IServiceCollection AddNamedScoped<TKey, TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            TImplementation AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddScoped(AdaptedFactory);
        }
    }
}
