using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    /// <summary>
    /// Marks the type to to be registered in <see cref="IServiceCollection"/> as implementation type for all implemented interfaces.
    /// </summary>
    public class RegisterAsImplementedInterfacesAttribute : RegisterBase
    {
        /// <summary>
        /// Creates an instance of the attribute.
        /// </summary>
        /// <param name="lifetime">Service instance lifetime.</param>
        public RegisterAsImplementedInterfacesAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
        }

        /// <summary>
        /// Registration lifetime.
        /// </summary>
        public ServiceLifetime Lifetime { get; }

        /// <inheritdoc/>
        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var interfaces = target.GetInterfaces();

            if (interfaces.Length == 0)
            {
                throw new ArgumentException("Type should implement at least one interface to be registered.", nameof(target));
            }

            foreach (var @interface in interfaces)
            {
                var descriptor = ServiceDescriptor.Describe(@interface, target, Lifetime);

                services.Add(descriptor);
            }
        }
    }
}