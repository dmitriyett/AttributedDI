using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    public class RegisterAsSelfAttribute : RegisterBase
    {
        public RegisterAsSelfAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = ServiceDescriptor.Describe(target, target, Lifetime);

            services.Add(descriptor);
        }
    }
}