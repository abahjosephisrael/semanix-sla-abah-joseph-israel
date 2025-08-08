using FormsEngine.Shared.Middlewares;
using FormsEngine.Shared.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace FormsEngine.Shared.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
    }
    
    public static void UseTenantValidationMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<TenantValidationMiddleware>();
    }

    public static void UseSharedMiddlewares(this IApplicationBuilder app)
    {
        app.UseSerilogRequestLogging();
        app.UseErrorHandlingMiddleware();
        app.UseTenantValidationMiddleware();
    }

    public static void ConfigureHost(WebApplicationBuilder appBuilder)
    {
        var hostBuilder = appBuilder.Host as IHostBuilder;
        // Configure Serilog
        hostBuilder.UseSerilog(LoggerSetting.Configure());

        // Configure the shutdown timeout
        hostBuilder.ConfigureHostOptions(options =>
        {
            options.ShutdownTimeout = TimeSpan.FromSeconds(30);
        });
    }
}
