using System;
using System.Reflection;

namespace AssemblyWithMultipleTypesToRegister
{
    public class AssemblyWithMultipleTypesToRegisterDescriptor
    {
        public Assembly Assembly { get; } = typeof(AssemblyWithMultipleTypesToRegisterDescriptor).Assembly;

        public int ExpectedNumberOfRegistrations { get; } = 2;
    }
}
