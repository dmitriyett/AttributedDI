using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public static class RegistrationServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesFromAssemblyContainingType<T>(this IServiceCollection services)
        {
            return AddServicesFromAssembly(services, typeof(T).Assembly);
        }

        public static IServiceCollection AddServicesFromAssemblyContainingType(this IServiceCollection services, Type type)
        {
            return AddServicesFromAssembly(services, type.Assembly);
        }

        public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
        {
            var typesToRegister = assembly.GetTypes().Where(ContainsRegisterAttribute).ToArray();

            foreach (var type in typesToRegister)
            {
                PerformRegistration(services, type);
            }

            return services;
        }

        private static bool ContainsRegisterAttribute(Type type)
        {
            var registerBaseType = typeof(RegisterBase);

            return type.CustomAttributes.Any(a => typeof(RegisterBase).IsAssignableFrom(a.AttributeType));
        }

        private static void PerformRegistration(IServiceCollection services, Type type)
        {
            var attributes = type.GetCustomAttributes<RegisterBase>();

            foreach (var attribute in attributes)
            {
                attribute.PerformRegistration(services, type);
            }
        }
    }
}