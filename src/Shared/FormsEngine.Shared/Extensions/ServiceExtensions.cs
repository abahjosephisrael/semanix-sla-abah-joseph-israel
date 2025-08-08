using System.Reflection;
using Asp.Versioning;
using FluentValidation;
using FormsEngine.Shared.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FormsEngine.Shared.Extensions;

public static class ServiceExtensions
{
    public static void AddSharedLayer(this IServiceCollection services, Assembly assembly)
    {

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddValidatorsFromAssembly(assembly);
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


        services.AddApiVersioning(config =>
        {
            // Specify the default API Version as 1.0
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // If the client hasn't specified the API version in the request, use the default API version number 
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });
    }

}
