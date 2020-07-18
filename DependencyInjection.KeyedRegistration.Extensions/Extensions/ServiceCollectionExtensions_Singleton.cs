using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection.KeyedRegistration.Extensions.Extensions
{
    public static partial class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType"/> with a
        /// factory specified in <paramref name="implementationFactory"/> to the
        /// specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="ServiceLifetime.Singleton"/>
        public static IServiceCollection AddNamedSingleton<TKey>(
            this IServiceCollection services,
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            TKey key)
        {
            object AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return
                services
                    .AddSingleton
                    (
                        serviceType, 
                        AdaptedFactory
                    );
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with a
        /// factory specified in <paramref name="implementationFactory" /> to the
        /// specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
        public static IServiceCollection AddNamedSingleton<TKey,TService>(
          this IServiceCollection services,
          Func<IServiceProvider, TService> implementationFactory,
          TKey key)
          where TService : class
        {
            TService AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return 
                services
                    .AddSingleton
                    (
                        typeof(TService), 
                        AdaptedFactory
                    );
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// implementation type specified in <typeparamref name="TImplementation" /> using the
        /// factory specified in <paramref name="implementationFactory" /> to the
        /// specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>/// 
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the service to.</param>
        /// <param name="implementationFactory">The factory that creates the service.</param>
        /// <param name="key">The name or key for the instance</param>/// 
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
        public static IServiceCollection AddNamedSingleton<TKey, TService, TImplementation>(
          this IServiceCollection services,
          Func<IServiceProvider, TImplementation> implementationFactory, TKey key)
          where TService : class
          where TImplementation : class, TService
        {
            TImplementation AdaptedFactory(IServiceProvider provider) => NamedInstanceAdapter<TKey>.Create(implementationFactory.Invoke(provider), key);

            return 
                services
                    .AddSingleton(AdaptedFactory);
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <paramref name="serviceType" /> with an
        /// instance specified in <paramref name="implementationInstance" /> to the
        /// specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the service to.</param>
        /// <param name="serviceType">The type of the service to register.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="key">The name or key for the instance</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
        public static IServiceCollection AddNamedSingleton<TKey>(this IServiceCollection services, Type serviceType, object implementationInstance, TKey key)
        {
            return
                services
                    .AddSingleton
                    (
                        serviceType,
                        NamedInstanceAdapter<TKey>.Create(serviceType, implementationInstance, key)
                    );
        }

        /// <summary>
        /// Adds a singleton service of the type specified in <typeparamref name="TService" /> with an
        /// instance specified in <paramref name="implementationInstance" /> to the
        /// specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key</typeparam>/// 
        /// <typeparam name="TService">The type of the service to add.</typeparam>
        /// <param name="services">The <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" /> to add the service to.</param>
        /// <param name="implementationInstance">The instance of the service.</param>
        /// <param name="key">The name or key for the instance</param>/// 
        /// <returns>A reference to this instance after the operation has completed.</returns>
        /// <seealso cref="F:Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton" />
        public static IServiceCollection AddNamedSingleton<TKey,TService>(
          this IServiceCollection services,
          TService implementationInstance,
          TKey key)
          where TService : class
        {
            return 
                services
                    .AddSingleton
                    (
                        NamedInstanceAdapter<TKey>.Create(implementationInstance, key)
                    );
        }
    }
}
