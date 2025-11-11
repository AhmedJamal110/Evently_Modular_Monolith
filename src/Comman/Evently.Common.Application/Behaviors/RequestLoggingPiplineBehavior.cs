using Evently.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Serilog.Context;

namespace Evently.Common.Application.Behaviors;
internal sealed class RequestLoggingPiplineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPiplineBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        string moduleName = GetModuleName(typeof(TRequest).FullName!);

        string requestName = typeof(TRequest).Name;

        using (LogContext.PushProperty("module", moduleName))
        {
            _logger.LogInformation("Handling {RequestName} in module {ModuleName}", requestName, moduleName);
            
            TResponse response = await next(cancellationToken);

            if(response.IsSuccess)
            {
                _logger.LogInformation("Handled {RequestName} in module {ModuleName} successfully", requestName, moduleName);
            }
            else
            {
                using (LogContext.PushProperty("Errors", response.Error, true))
                {
                    _logger.LogError("Handling {RequestName} in module {ModuleName} failed with error: {Error}", requestName, moduleName, response.Error);
                }
            }

            return response;

        }

    }

    private static string GetModuleName(string requestName)
    {
        return requestName.Split('.')[2];
    }

}
