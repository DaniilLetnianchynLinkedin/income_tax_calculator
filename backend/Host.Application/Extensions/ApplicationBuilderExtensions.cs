using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;

namespace Host.Application.Extensions;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseProductionExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                if (exceptionHandlerPathFeature?.Error != null)
                {
                    using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
                    var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger>();
                    logger.LogError(exceptionHandlerPathFeature.Error, context.TraceIdentifier);
                    await HandleExceptionAsync(context, exceptionHandlerPathFeature.Error);
                }
            });
        });
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            Status = statusCode,
            Detail = exception.Message,
        };

        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            _ => StatusCodes.Status500InternalServerError,
        };
}