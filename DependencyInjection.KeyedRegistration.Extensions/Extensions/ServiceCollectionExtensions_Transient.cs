using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddNamedTransient<TKey, TService, TImplementation>(this IServiceCollection services, TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            return
                services
                    .AddNamedTransient
                    (
                        typeof(TService),
                        typeof(TImplementation),
                        key
                    );
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with an
        /// implementation of the type specified in <paramref name="implementationType"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddNamedTransient<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Type implementationType,
            TKey key)
        {
            object AdaptedFactory(IServiceProvider provider) =>
                NamedInstanceAdapter<TKey>
                    .Create
                    (
                        serviceType,
                        ActivatorUtilities.GetServiceOrCreateInstance(provider, implementationType),
                        key
                    );

            services
                .Add
                (
                    new ServiceDescriptor
                    (
                        serviceType,
                        AdaptedFactory,
                        ServiceLifetime.Transient
                    )
                );

            return
                services;
        }

        /// <summary>
        /// Adds a transient service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddNamedTransient<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            object AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddTransient
                    (
                        serviceType,
                        AdaptedFactory
                    );
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddNamedTransient<TKey, TService, TImplementation>(
            this IServiceCollection services,
            Func<IServiceProvider, TImplementation> implementationFactory,
            TKey key)
            where TService : class
            where TImplementation : class, TService
        {
            TImplementation AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddTransient(AdaptedFactory);
        }

        /// <summary>
        /// Adds a transient service of the type specified in <typeparamref name="TService"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Transient"/>
        public static IServiceCollection AddNamedTransient<TKey,TService>(
            this IServiceCollection services,
            Func<IServiceProvider, TService> implementationFactory,
            TKey key)
            where TService : class
        {
            TService AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddTransient
                    (
                        typeof(TService),
                        AdaptedFactory
                    );
        }
    }
}
