using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAsSelfAttribute : RegisterBase
    {
        public RegisterAsSelfAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            this.Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = ServiceDescriptor.Describe(target, target, Lifetime);

            services.Add(descriptor);
        }
    }
}