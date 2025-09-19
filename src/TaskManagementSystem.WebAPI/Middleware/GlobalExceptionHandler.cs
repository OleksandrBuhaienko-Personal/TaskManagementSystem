using System.Net;
using System.Reflection.Metadata;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace TaskManagementSystem.WebAPI.Middleware;

public class GlobalExceptionHandler : IMiddleware
{
  private readonly ILogger<GlobalExceptionHandler> _logger;

  public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) => _logger = logger;

  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception exception)
    {
      _logger.LogError(exception, exception.Message);

      context.Response.StatusCode =
        (int)HttpStatusCode.BadRequest;

      ProblemDetails problem = new()
      {
        Status = (int)HttpStatusCode.BadRequest,
        Type = "Internal server error",
        Title = "Error occurred while processing your request.",
        Detail = exception.Message
      };

      string json = JsonSerializer.Serialize(problem);

      context.Response.ContentType = "application/json";

      await context.Response.WriteAsync(json);
    }
  }
}
