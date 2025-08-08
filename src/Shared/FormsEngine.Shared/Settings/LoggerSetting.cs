using System.Reflection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace FormsEngine.Shared.Settings;

public static class LoggerSetting
{
    public static Action<HostBuilderContext, LoggerConfiguration> Configure() =>
        (context, configuration) =>
        {
            configuration
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .MinimumLevel.Error()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .Enrich.WithProperty("ContentRootPath", context.HostingEnvironment.ContentRootPath)
                .Enrich.WithEnvironmentName()
                .Enrich.FromLogContext()
                .WriteTo.Debug()
                .WriteTo.Console();
        };

    public static string AppInfo => $"Service::: {Assembly.GetEntryAssembly().GetName().Name} - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}";

}
