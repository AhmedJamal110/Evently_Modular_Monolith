using System;
using Microsoft.AspNetCore.Diagnostics;

namespace Evently.Api.Middlewares;

public sealed class GlobalExceptionHandler(
    IProblemDetailsService _problemDetailsService,
    IHostEnvironment _environment) : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception,
        CancellationToken cancellationToken)
    {

        return _problemDetailsService.TryWriteAsync(new ProblemDetailsContext()
        {
            Exception = exception,
            HttpContext = httpContext,
            ProblemDetails = new()
            {
                Title = "Internal Server Error",
                Status = StatusCodes.Status500InternalServerError,
                Detail = _environment.IsDevelopment()
                    ? exception.Message
                    : "An unexpected error occurred. Please try again later."

            }
        });

    }
}
