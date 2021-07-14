using System;
using AttributedDI.Tests.Stubs;
using AutoFixture.Xunit2;
using FluentAssertions;
using Xunit;

namespace AttributedDI.Tests
{
    public class RegisterAsImplmenentedInterfacesTests
    {
        // throws when class doesn't implement any interfaces
        // registers for all implemented interfaces
        // lifetime is correct
        // ServiceType is correct
        // ImplementationType is correct        
        [Theory]
        [InlineAutoData]
        public void Throws_When_Service_Doesnt_Implement_Any_Interfaces(Type serviceWithNoInterfaces, ServiceCollectionStub services, RegisterAsImplementedInterfacesAttribute sut)
        {
            // act
            Action act = () => sut.PerformRegistration(services, serviceWithNoInterfaces);

            // assert
            act.Should().Throw<ArgumentException>().WithMessage("Type should implement at least one interface*");
        }

        [Theory]
        [InlineAutoData(typeof(StubWithTwoImplmenetedInterfaces))]
        public void Creates_Registration_For_All_Implemented_Interfaces(Type serviceWithInterfaces, ServiceCollectionStub services, RegisterAsImplementedInterfacesAttribute sut)
        {
            // act
            sut.PerformRegistration(services, serviceWithInterfaces);

            // assert
            services.Should().HaveCount(serviceWithInterfaces.GetInterfaces().Length, "There should be as many registrations as there interfaces");
        }
    }
}