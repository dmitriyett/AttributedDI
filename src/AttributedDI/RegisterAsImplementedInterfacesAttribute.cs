using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    public class RegisterAsImplementedInterfacesAttribute : RegisterBase
    {
        public RegisterAsImplementedInterfacesAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }

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