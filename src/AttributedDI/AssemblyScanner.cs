using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributedDI
{
    public record AssemblyScanResult(Type Service, RegisterBase RegisterAttribute);

    public class AssemblyScanner
    {
        private static readonly Type RegisterBaseType = typeof(RegisterBase);

        public AssemblyScanResult[] Scan(Assembly assembly)
        {
            var results = assembly.GetTypes()
                .Where(ContainsRegisterAttribute)
                .SelectMany(ToScanResult)
                .ToArray();

            return results;
        }

        private static bool ContainsRegisterAttribute(Type type)
        {
            return type.CustomAttributes.Any(a => RegisterBaseType.IsAssignableFrom(a.AttributeType));
        }

        private static IEnumerable<AssemblyScanResult> ToScanResult(Type type)
        {
            var attributes = type.GetCustomAttributes<RegisterBase>();

            return attributes.Select(a => new AssemblyScanResult (type, a));
        }
    }
}