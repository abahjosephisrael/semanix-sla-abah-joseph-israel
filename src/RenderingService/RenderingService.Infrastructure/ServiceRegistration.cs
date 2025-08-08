using FormsEngine.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RenderingService.Infrastructure.Repositories;
using System.Data;

namespace RenderingService.Infrastructure;

public static class ServiceRegistration
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? Environment.GetEnvironmentVariable("DefaultConnection") : configuration.GetConnectionString("DefaultConnection");
        services.AddHttpClient();
        services.AddScoped<IDbConnection>(sp =>new NpgsqlConnection(connString));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

}
