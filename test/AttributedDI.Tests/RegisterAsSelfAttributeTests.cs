using System;
using System.Linq;
using AttributedDI.Tests.Stubs;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AttributedDI.Tests
{
    public class RegisterAsSelfAttributeTests
    {
        [Theory]
        [AutoData]
        public void Adds_Service_To_Container(ServiceCollectionStub services, Type typeToRegister, RegisterAsSelfAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            services.Should().HaveCount(1, "Single service should be registered");
        }

        [Theory]
        [AutoData]
        public void Added_Service_Has_Correct_Lifetime(ServiceCollectionStub services, Type typeToRegister, [Frozen] ServiceLifetime expectedLifetime, RegisterAsSelfAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.Lifetime.Should().Be(expectedLifetime, "Service should be registered with specified lifetime");
        }

        [Theory]
        [AutoData]
        public void Added_Service_Has_Correct_Service_Type(ServiceCollectionStub services, Type typeToRegister, RegisterAsSelfAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ServiceType.Should().Be(typeToRegister, "Correct service should be registered");
        }

        [Theory]
        [AutoData]
        public void Added_Service_Has_Correct_Implementation_Type(ServiceCollectionStub services, Type typeToRegister, RegisterAsSelfAttribute sut)
        {
            // act
            sut.PerformRegistration(services, typeToRegister);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ImplementationType.Should().Be(typeToRegister, "Service should be registered as self in container");
        }
    }
}