using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class RegisterBase : Attribute
    {
        protected RegisterBase(ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }

        public abstract void PerformRegistration(IServiceCollection services, Type target);
    }
}