using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class RegisterAttribute : Attribute
    {
        private readonly Lifetime lifetime;

        public RegisterAttribute(Lifetime lifetime)
        {
            this.lifetime = lifetime;
        }

        protected virtual void PerformRegistration(IServiceCollection services, Type target)
        {
            var serviceDescriptor = CreateDescriptor(services, target);

            services.Add(serviceDescriptor);
        }

        protected virtual ServiceDescriptor CreateDescriptor(IServiceCollection services, Type target)
        {
            var serviceLifetime = MapLifetime(lifetime);

            return ServiceDescriptor.Describe(target, target, serviceLifetime);
        }

        protected virtual ServiceLifetime MapLifetime(Lifetime lifetime) => lifetime switch
        {
            Lifetime.Scoped => ServiceLifetime.Scoped,
            Lifetime.Singleton => ServiceLifetime.Singleton,
            Lifetime.Transient => ServiceLifetime.Transient,
            _ => throw new NotSupportedException("Specified lifetime not supported")
        };
    }
}