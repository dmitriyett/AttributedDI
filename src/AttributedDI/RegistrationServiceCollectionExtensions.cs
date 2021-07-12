using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    // namespace Microsoft.Extensions.DependencyInjection
    public static class RegistrationServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesFromExecutingAssembly(this IServiceCollection services)
        {

        }

        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {

        }

        public static IServiceCollection AddServicesFromAssemblyContainingType<T>(this IServiceCollection services)
        {

        }

        public static IServiceCollection AddServicesFromAssemblyContainingType(this IServiceCollection services, Type type)
        {

        }
    }
}