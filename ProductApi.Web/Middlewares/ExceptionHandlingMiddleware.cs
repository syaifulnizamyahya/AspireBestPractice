using FluentResults;
using System.Net;
using System.Text.Json;

namespace ProductApi.Web.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var error = Result.Fail(new Error(ex.Message).CausedBy(ex.InnerException));
                await HandleExceptionAsync(context, error);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Result errorResult)
        {
            var statusCode = HttpStatusCode.InternalServerError;

            var response = new
            {
                message = "An unexpected error occurred.",
                errors = errorResult.Errors.Select(e => e.Message)
            };

            var payload = JsonSerializer.Serialize(response);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(payload);
        }
    }
}
