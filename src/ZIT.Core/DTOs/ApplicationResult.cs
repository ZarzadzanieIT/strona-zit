using System.Net;

namespace ZIT.Core.DTOs;

public class ApplicationResult
{
    public static ApplicationResult<T?> Success<T>(HttpStatusCode statusCode, params string[] messages) =>
        new(default, statusCode, false, messages);

    public static ApplicationResult<T?> Success<T>(T value, HttpStatusCode statusCode, params string[] messages) =>
        new(value, statusCode, false, messages);

    public static ApplicationResult<T?> Fail<T>(HttpStatusCode statusCode, params string[] messages) =>
        new(default, statusCode, true, messages);
}

public class ApplicationResult<T>
{
    public T? Value { get; }
    public string[] Messages { get; }
    public HttpStatusCode Status { get; }
    public bool Failed { get; set; }

    internal ApplicationResult(T value, HttpStatusCode status, bool failed, params string[] messages)
    {
        Value = value;
        Messages = messages;
        Status = status;
        Failed = failed;
    }
}