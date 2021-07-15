using AssemblyWithMultipleTypesToRegister;
using AssemblyWithNoTypesToRegister;
using AssemblyWithSingleTypeWithMultipleRegisterAttributes;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AttributedDI.Tests
{
    public class AssemblyScannerTests
    {
        [Theory]
        [AutoData]
        public void When_Assembly_Doesnt_Have_Any_Types_To_Register_Returns_Empty_List(
            AssemblyWithNoTypesToRegisterDescriptor assemblyDescriptor,
            AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyDescriptor.Assembly);

            // assert
            result.Should().BeEmpty("There is no types to register present in assembly");
        }

        [Theory]
        [AutoData]
        public void When_Type_Has_Multiple_Register_Attributes_Should_Return_Multiple_Results(
            AssemblyWithSingleTypeWithMultipleRegisterAttributesDescriptor assemblyDescriptor,
            AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyDescriptor.Assembly);

            // assert
            result.Should().HaveCount(assemblyDescriptor.ExpectedNumberOfRegistrations, "There should be as many results as register attributes on type");
        }

        [Theory]
        [AutoData]
        public void Returns_Result_For_Every_Type_With_Register_Attribute(
            AssemblyWithMultipleTypesToRegisterDescriptor assemblyDescriptor,
            AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyDescriptor.Assembly);

            // assert
            result.Should().HaveCount(assemblyDescriptor.ExpectedNumberOfRegistrations, "There should be as many results as types to register");
        }
    }
}