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
        [InlineAutoData(typeof(ServiceStub), typeof(ImplementationStub))]
        public void Added_Service_Has_Correct_Service_Type(Type registerAsType, Type registerType, ServiceCollectionStub services, Fixture fixture)
        {
            // arrange
            // workaround for autofixture supplying typeof(object) for all type instances.
            fixture.Inject(registerAsType);
            var sut = fixture.Create<RegisterAsAttribute>();

            // act
            sut.PerformRegistration(services, registerType);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ServiceType.Should().Be(registerAsType, "Correct service should be registered");
        }

        [Theory]
        [InlineAutoData(typeof(ServiceStub), typeof(ImplementationStub))]
        public void Added_Service_Has_Correct_Implementation_Type(Type registerAsType, Type registerType, ServiceCollectionStub services, Fixture fixture)
        {
            // arrange
            // workaround for autofixture supplying typeof(object) for all type instances.
            fixture.Inject(registerAsType);
            var sut = fixture.Create<RegisterAsAttribute>();

            // act
            sut.PerformRegistration(services, registerType);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.ImplementationType.Should().Be(registerType, "Service should be registered as self in container");
        }
    }
}
