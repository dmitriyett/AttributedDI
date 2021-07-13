using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAsAttribute : RegisterBase
    {
        private readonly Type serviceType;

        public RegisterAsAttribute(Type serviceType, ServiceLifetime lifetime = ServiceLifetime.Transient) : base(lifetime)
        {
            this.serviceType = serviceType;
        }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = ServiceDescriptor.Describe(serviceType, target, Lifetime);

            services.Add(descriptor);
        }
    }
}