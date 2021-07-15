using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace AttributedDI
{
    /// <summary>
    /// Extension methods on <see cref="IServiceCollection"/> to register services marked with <see cref="RegisterBase"/> family of attributes.
    /// </summary>
    public static class RegistrationServiceCollectionExtensions
    {
        private static readonly AssemblyScanner AssemblyScanner = new();

        /// <summary>
        /// Scans an assembly containing <typeparamref name="T"/> for types marked with <see cref="RegisterAsAttribute"/> family of attributes and registers them in <paramref name="services"/>.
        /// </summary>
        /// <typeparam name="T">Type located in assembly with services to be registered.</typeparam>
        /// <param name="services">Services collection to register services into.</param>
        /// <returns>Services collection to chain multiple calls.</returns>
        public static IServiceCollection AddServicesFromAssemblyContainingType<T>(this IServiceCollection services)
        {
            return AddServicesFromAssembly(services, typeof(T).Assembly);
        }

        /// <summary>
        /// Scans an assembly containing <paramref name="type"/> for types marked with <see cref="RegisterAsAttribute"/> family of attributes and registers them in <paramref name="services"/>.
        /// </summary>
        /// <param name="services">Services collection to register services into.</param>
        /// <param name="type">Type located in assembly with services to be registered.</param>
        /// <returns>Services collection to chain multiple calls.</returns>
        public static IServiceCollection AddServicesFromAssemblyContainingType(this IServiceCollection services, Type type)
        {
            return AddServicesFromAssembly(services, type.Assembly);
        }

        /// <summary>
        /// Scans <paramref name="assembly"/> for types marked with <see cref="RegisterAsAttribute"/> family of attributes and registers them in <paramref name="services"/>.
        /// </summary>
        /// <param name="services">Services collection to register services into.</param>
        /// <param name="assembly">Assembly with </param>
        /// <returns>Services collection to chain multiple calls.</returns>
        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var scanResults = AssemblyScanner.Scan(assembly);

            foreach (var scanResult in scanResults)
            {
                scanResult.RegisterAttribute.PerformRegistration(services, scanResult.Service);
            }

            return services;
        }
    }
}