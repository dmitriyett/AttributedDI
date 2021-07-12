using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAsAttribute : RegisterBase
    {
        private readonly Type serviceType;

        public RegisterAsAttribute(Lifetime lifetime, Type serviceType) : base(lifetime)
        {
            this.serviceType = serviceType;
        }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = CreateDescriptor(serviceType, target);

            services.Add(descriptor);
        }
    }
}