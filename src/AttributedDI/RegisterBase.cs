using Microsoft.Extensions.DependencyInjection;
using System;

namespace AttributedDI
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true, Inherited = true)]
    public abstract class RegisterBase : Attribute
    {
        public abstract void PerformRegistration(IServiceCollection services, Type target);
    }
}