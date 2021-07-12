using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class RegisterBase : Attribute
    {
        protected RegisterBase(Lifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public Lifetime Lifetime { get; }

        public abstract void PerformRegistration(IServiceCollection services, Type target);

        protected virtual ServiceDescriptor CreateDescriptor(Type serviceType, Type implType)
        {
            var serviceLifetime = MapLifetime(Lifetime);

            return ServiceDescriptor.Describe(serviceType, implType, serviceLifetime);
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