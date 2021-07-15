using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributedDI
{
    /// <summary>
    /// Assembly scan result.
    /// </summary>
    /// <param name="Service">Service to register</param>
    /// <param name="RegisterAttribute">Attached registration attribute</param>
    public record AssemblyScanResult(Type Service, RegisterBase RegisterAttribute);

    /// <summary>
    /// Scans an assembly for types that need to be registered in <see cref="Microsoft.Extensions.DependencyInjection.IServiceCollection"/>.
    /// </summary>
    public class AssemblyScanner
    {
        private static readonly Type RegisterBaseType = typeof(RegisterBase);

        /// <summary>
        /// Scan an assembly for types to be registered.
        /// </summary>
        /// <param name="assembly">Assembly to be scanned</param>
        /// <returns>List of types to be registered.</returns>
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

            return attributes.Select(a => new AssemblyScanResult(type, a));
        }
    }
}