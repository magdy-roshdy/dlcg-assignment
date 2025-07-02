using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace GameCatalogue.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Unhandled exception for request {context.Request.Method} {context.Request.Path}");

                var problem = new ProblemDetails
                {
                    Type = "internal-server-error",
                    Title = "Internal server error occured.",
                    Status = (int)HttpStatusCode.InternalServerError,
                    Detail = "Please contact support."
                };

                context.Response.Clear();
                context.Response.StatusCode = problem.Status.Value;
                context.Response.ContentType = "application/problem+json";

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(problem, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
