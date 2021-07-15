using System.Reflection;

namespace AssemblyWithSingleTypeWithMultipleRegisterAttributes
{
    public class AssemblyWithSingleTypeWithMultipleRegisterAttributesDescriptor
    {
        public Assembly Assembly { get; } = typeof(AssemblyWithSingleTypeWithMultipleRegisterAttributesDescriptor).Assembly;

        public int ExpectedNumberOfRegistrations { get; } = 2;
    }
}
