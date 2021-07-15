using AttributedDI.Tests.Stubs;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace AttributedDI.Tests
{
    public class RegisterAsImplmenentedInterfacesTests
    {
        [Theory]
        [InlineAutoData(typeof(StubWithNoImplementedInterfaces))]
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

        [Theory]
        [InlineAutoData(typeof(StubWithSingleImplementedInterface))]
        public void Added_Service_Has_Correct_Lifetime(Type service, ServiceCollectionStub services, [Frozen] ServiceLifetime expectedLifetime, RegisterAsImplementedInterfacesAttribute sut)
        {
            // act
            sut.PerformRegistration(services, service);

            // assert
            var descriptor = services.FirstOrDefault();

            descriptor?.Lifetime.Should().Be(expectedLifetime, "Service should be registered with specified lifetime");
        }

        [Theory]
        [InlineAutoData(typeof(StubWithTwoImplmenetedInterfaces))]
        public void Added_Services_Have_Correct_Service_Type(Type service, ServiceCollectionStub services, RegisterAsImplementedInterfacesAttribute sut)
        {
            // act
            sut.PerformRegistration(services, service);

            // assert
            services.Should().Equal(service.GetInterfaces(),
                (serviceDescriptor, @interface) => serviceDescriptor.ServiceType == @interface,
                "Service type should be the implemented interface");
        }

        [Theory]
        [InlineAutoData(typeof(StubWithTwoImplmenetedInterfaces))]
        public void Added_Services_Have_Correct_Implementation_Type(Type service, ServiceCollectionStub services, RegisterAsImplementedInterfacesAttribute sut)
        {
            // act
            sut.PerformRegistration(services, service);

            // assert
            services.Should().OnlyContain(sd => sd.ImplementationType == service, "Implementation type should be the type being registered");
        }
    }
}