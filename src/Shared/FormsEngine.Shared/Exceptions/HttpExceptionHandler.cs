using System.Net;
using FormsEngine.Shared.Wrappers;
using Newtonsoft.Json;


namespace FormsEngine.Shared.Exceptions;

public class HttpExceptionHandler
{
    public static Exception GetException(HttpResponseMessage response)
    {
        var json = response.Content.ReadAsStringAsync().Result;

        string? errorMessage;
        try
        {
            var readyData = JsonConvert.DeserializeObject<Response<string>>(json);
            errorMessage = readyData?.Message;
        }
        catch
        {
            errorMessage = json;
        }
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "Internal server error occured";
            return new HttpServerErrorException(message);
        }
        else if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "Bad request error occured";
            return new HttpBadRequestException(message);
        }
        else if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "Unauthorised request";
            return new HttpUnauthorisedException(message);
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "The requested resource is not found";
            return new HttpNotFoundException(message);
        }
        else if (response.StatusCode == HttpStatusCode.Conflict)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "The request created a conflict";
            return new HttpConflictException(message);
        }
        else if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "You are not authorized to access this resource";
            return new HttpBadForbiddenException(message);
        }
        else
        {
            var message = !string.IsNullOrEmpty(errorMessage) ? errorMessage : "Unknown request error occured";
            return new Exception($"{response.StatusCode} - {message}");
        }
    }
}