using FormsEngine.Shared.Exceptions;
using FormsEngine.Shared.Settings;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace FormsEngine.Shared.Middlewares;

public class TenantValidationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {

        var path = context.Request.Path.ToString().ToLower();
        // Skip tenant validation for metrics and health checks
        if (path.Contains("metrics") || path.Contains("metric") || path.Contains("health") || path.Contains("swagger") || path.Contains("api-docs"))
        {
            await next(context);
            return;
        }
        var tenantId = context.Request.Headers["X-Tenant-Id"];
        var entityId = context.Request.Headers["X-Entity-Id"];

        if (string.IsNullOrEmpty(tenantId))
        {
            Log.Error("X-Tenant-Id missing from the request", LoggerSetting.AppInfo);
            throw new HttpBadRequestException("Tenant ID is required.");
        }
        else
        {
            TenantSettings.TenantId = tenantId.ToString();
            if (!string.IsNullOrEmpty(entityId))
            {
                TenantSettings.EntityId = entityId.ToString();
            }
            else
            {
                TenantSettings.EntityId = null;
            }
            Log.Information("Tenant ID set to {TenantId}", TenantSettings.TenantId, LoggerSetting.AppInfo);
        }
        await next(context);
    }
}
