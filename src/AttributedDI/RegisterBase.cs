using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    /// <summary>
    /// Base class for all registration attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class RegisterBase : Attribute
    {
        /// <summary>
        /// Performs service registration.
        /// </summary>
        /// <param name="services">Services container.</param>
        /// <param name="target">Service to register.</param>
        public abstract void PerformRegistration(IServiceCollection services, Type target);
    }
}