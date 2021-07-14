using System;
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
        public AssemblyScanner(Assembly assembly)
        {

        }

        public AssemblyScanResult[] Scan()
        {
            throw new NotImplementedException();
        }
    }
}