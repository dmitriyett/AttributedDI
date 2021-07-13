using System;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class RegisterBase : Attribute
    {
        protected RegisterBase(ServiceLifetime lifetime)
        {
            if (!Enum.IsDefined(typeof(ServiceLifetime), lifetime))
            {
                throw new ArgumentException("Specified lifetime is not supported.", nameof(lifetime));
            }

            Lifetime = lifetime;
        }

        public ServiceLifetime Lifetime { get; }

        public abstract void PerformRegistration(IServiceCollection services, Type target);
    }
}