using Core.Enums;
using Core.Exceptions;
using Core.Models;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Extensions;

public static class WebAppExceptionHandler
{
    public static WebApplication MapExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(c => c.Run(async context =>
        {
            var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
            switch (exception)
            {
                case null:
                    return;
                case DemoUserNotFoundException:
                    context.Response.StatusCode = 404;
                    break;
                case DemoServiceException:
                {
                    context.Response.StatusCode = 400;
                    var errorType = exception.InnerException is DemoRepositoryException
                        ? ErrorType.DatabaseFailed
                        : ErrorType.BusinessRuleFailed;

                    await context.Response.WriteAsJsonAsync(new Error
                    {
                        ErrorCode = errorType,
                        Message = exception.Message,
                        Timestamp = DateTime.UtcNow
                    });
                    break;
                }
                case DemoRepositoryException:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new Error
                    {
                        ErrorCode = ErrorType.UnexpectedDatabaseFailure,
                        Message = exception.Message,
                        Timestamp = DateTime.UtcNow
                    });
                    break;
                case DemoBaseException:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new Error
                    {
                        ErrorCode = ErrorType.UnmappedApplicationFailure,
                        Message = "Unmapped application error.",
                        Timestamp = DateTime.UtcNow
                    });
                    break;
                default:
                    context.Response.StatusCode = 500;
                    await context.Response.WriteAsJsonAsync(new Error
                    {
                        ErrorCode = ErrorType.UnexpectedFailure,
                        Message = "Unexpected application error.",
                        Timestamp = DateTime.UtcNow
                    });
                    break;
            }
            
            context.Response.ContentType = "application/json";
            await context.Response.CompleteAsync();
        }));
        return app;
    }
}