using System;
using System.Linq;
using System.Reflection;

namespace AttributedDI
{
    public record AssemblyScanResult(Type Service, RegisterBase registerAttribute);

    public class AppDomainScanner
    {
        public AppDomainScanner()
        {

        }
    }

    public class AssemblyScanner
    {
        public AssemblyScanResult[] Scan(Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(ContainsRegisterAttribute)
                .Select(t => t.GetCustomAttributes<RegisterBase>());

            throw new NotImplementedException();
        }

        private static bool ContainsRegisterAttribute(Type type)
        {
            var registerBaseType = typeof(RegisterBase);

            return type.CustomAttributes.Any(a => typeof(RegisterBase).IsAssignableFrom(a.AttributeType));
        }
    }
}