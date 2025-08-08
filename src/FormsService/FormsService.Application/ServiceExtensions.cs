using FormsEngine.Shared.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FormsService.Application;

public static class ServiceExtensions
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        // The shared layer is added to the application layer to ensure that all shared services, behaviors, and configurations are available.
        services.AddSharedLayer(Assembly.GetExecutingAssembly());
    }
}
