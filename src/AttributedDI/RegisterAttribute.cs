using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    public class RegisterAttribute : RegisterBase
    {
        public RegisterAttribute(Lifetime lifetime) : base(lifetime)
        {
        }

        public override void PerformRegistration(IServiceCollection services, Type target)
        {
            var descriptor = CreateDescriptor(target, target);

            services.Add(descriptor);
        }
    }
}