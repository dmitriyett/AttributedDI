using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace AttributedDI.Tests.Stubs
{
    public class ServiceCollectionStub : List<ServiceDescriptor>, IServiceCollection
    {
    }
}