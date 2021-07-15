using AttributedDI;

namespace AssemblyWithSingleTypeWithMultipleRegisterAttributes
{
    [RegisterAs(typeof(Interface1))]
    [RegisterAs(typeof(Interface2))]
    public class TypeWithMultipleRegisterAttributes : Interface1, Interface2
    {
    }
}