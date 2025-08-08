using System.Text.Json.Serialization;

namespace FormsEngine.Shared.Wrappers;

public class Response<T>
{
    public Response()
    {
    }
    public Response(T data, string message = null)
    {
        Succeeded = true;
        Message = message;
        Data = data;
    }
    public Response(string message)
    {
        Succeeded = false;
        Message = message;
    }
    [JsonPropertyName("succeeded")]
    public bool Succeeded { get; set; }
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("errors")]
    public List<string> Errors { get; set; }
    [JsonPropertyName("data")]
    public T? Data { get; set; } = default;

    public static Response<T> Success(T data, string message = null)
    {
        return new Response<T>(data, message);
    }
    public static Response<T> Failure(string message)
    {
        return new Response<T>(message);
    }
}
