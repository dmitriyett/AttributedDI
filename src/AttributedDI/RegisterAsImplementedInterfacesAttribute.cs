using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAsImplementedInterfacesAttribute : RegisterBase
    {
        public RegisterAsImplementedInterfacesAttribute(Lifetime lifetime) : base(lifetime)
        {
        }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var interfaces = target.GetInterfaces();

            if (interfaces.Length == 0)
            {
                throw new ArgumentException("Type should implement at least one interface to be registered.", nameof(target));
            }

            foreach (var @interface in interfaces)
            {
                var descriptor = CreateDescriptor(@interface, target);

                services.Add(descriptor);
            }
        }
    }
}