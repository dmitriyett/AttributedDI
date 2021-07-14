using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAsAttribute : RegisterBase
    {        
        public RegisterAsAttribute(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            this.Lifetime = lifetime;
            this.ServiceType = serviceType;
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