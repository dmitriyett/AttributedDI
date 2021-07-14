using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AttributedDI.Tests.Stubs;
using AutoFixture;
using AutoFixture.AutoNSubstitute;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AttributedDI.Tests
{
    public class RegisterAsAttributeTests
    {
        [Theory]
        [AutoData]
        public void Adds_Service_To_Container(ServiceCollectionStub services, Type typeToRegister, RegisterAsAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            services.Should().HaveCount(1, "Single service should be registered");
        }

        [Theory]
        [AutoData]
        public void Added_Service_Has_Correct_Lifetime(ServiceCollectionStub services, Type typeToRegister, [Frozen] ServiceLifetime expectedLifetime, RegisterAsAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.Lifetime.Should().Be(expectedLifetime, "Service should be registered with specified lifetime");
        }

        [Theory]
        [AutoData]
        public void Added_Service_Has_Correct_Service_Type(ServiceCollectionStub services, Type typeToRegister, [Frozen] Type implementationType, RegisterAsAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ServiceType.Should().Be(typeToRegister, "Correct service should be registered");
        }        

        [Theory]
        [AutoDataWithCustomTypeGenerator]
        public void Added_Service_Has_Correct_Implementation_Type(ServiceCollectionStub services, Type typeToRegister, Type implementationType, RegisterAsAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ImplementationType.Should().Be(typeToRegister, "Service should be registered as self in container");
        }
    }
}
