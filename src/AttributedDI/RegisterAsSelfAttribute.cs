using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    /// <summary>
    /// Marks the type to to be registered in <see cref="IServiceCollection"/>.
    /// </summary>
    public class RegisterAsSelfAttribute : RegisterBase
    {
        /// <summary>
        /// Creates an instance of the attribute.
        /// </summary>
        /// <param name="lifetime">Service instance lifetime.</param>
        public RegisterAsSelfAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
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
            var descriptor = ServiceDescriptor.Describe(target, target, Lifetime);

            services.Add(descriptor);
        }
    }
}