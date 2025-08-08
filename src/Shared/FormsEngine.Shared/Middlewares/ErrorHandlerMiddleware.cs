using System.Net;
using System.Text.Json;
using FormsEngine.Shared.Exceptions;
using FormsEngine.Shared.Settings;
using FormsEngine.Shared.Wrappers;
using Microsoft.AspNetCore.Http;
using Serilog;


namespace FormsEngine.Shared.Middlewares;

public class ErrorHandlerMiddleware(RequestDelegate next)
{

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = new Response<string>() { Succeeded = false, Message = error.Message };

            switch (error)
            {
                case ApiException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case InvalidOperationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    responseModel.Errors = e.Errors;
                    responseModel.Message = e.Errors.Count > 0 ? e.Errors[0] : e.Message;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case HttpBadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case HttpUnauthorisedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case HttpNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    Log.Information(error, LoggerSetting.AppInfo);
                    break;
                case HttpConflictException:
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    Log.Error(error, LoggerSetting.AppInfo);
                    break;
                case HttpBadForbiddenException:
                    response.StatusCode = (int)HttpStatusCode.Forbidden;
                    Log.Warning(error, LoggerSetting.AppInfo);
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = "Unable to complete request, please try again.";
                    Log.Error(error, LoggerSetting.AppInfo);
                    break;
            }

            await response.WriteAsync(JsonSerializer.Serialize(responseModel));
        }
    }
}
