using System;
using System.Reflection;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AttributedDI.Tests
{
    public class AssemblyScannerTests
    {
        [Theory]
        [InlineAutoData]
        public void When_Assembly_Doesnt_Have_Any_Types_To_Register_Returns_Empty_List(Assembly assemblyToScan, AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyToScan);

            // assert
            result.Should().BeEmpty("There is no types to register present in assembly");
        }

        [Theory]
        public void When_Type_Has_Multiple_Register_Attributes_Should_Return_Multiple_Results(Assembly assemblyToScan, int numberOfRegisterAttributes, AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyToScan);

            // assert
            result.Should().HaveCount(numberOfRegisterAttributes, "There should be as many results as register attributes on type");
        }

        [Theory]
        public void Returns_Result_For_Every_Type_With_Register_Attribute(Assembly assemblyToScan, int numberOfTypesToRegister, AssemblyScanner sut)
        {
            // act
            var result = sut.Scan(assemblyToScan);

            // assert
            result.Should().HaveCount(numberOfTypesToRegister, "There should be as many results as types to register");
        }
    }
}