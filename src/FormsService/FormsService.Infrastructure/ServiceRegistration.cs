using FormsEngine.Shared.Interfaces;
using FormsService.Application.Interfaces;
using FormsService.Infrastructure.Persistence.Contexts;
using FormsService.Infrastructure.Persistence.Repositories;
using FormsService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FormsService.Infrastructure;

public static class ServiceRegistration
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        var connString = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
            ? Environment.GetEnvironmentVariable("DefaultConnection") : configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<FormsServiceDbContext>(options =>
        options.UseNpgsql(
           connString,
           b => b.MigrationsAssembly(typeof(FormsServiceDbContext).Assembly.FullName)));

        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddSingleton<IMetrics, PrometheusMetrics>();
    }

}
