using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    public class RegisterAsAttribute : RegisterBase
    {
        public RegisterAsAttribute(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }

        public Type ServiceType { get; }

        public ServiceLifetime Lifetime { get; }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = ServiceDescriptor.Describe(ServiceType, target, Lifetime);

            services.Add(descriptor);
        }
    }
}