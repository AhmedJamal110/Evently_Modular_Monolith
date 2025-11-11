using Evently.Common.Application.Exceptions;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Evently.Common.Application.Behaviors;
internal sealed class ExceptionHandlingPiplineBehavior<TRequest, TResponse>(
    ILogger<ExceptionHandlingPiplineBehavior<TRequest, TResponse>> _logger)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(
                exception, 
                "An unhandled exception occurred while processing {RequestType} with data: {@RequestData}", 
                typeof(TRequest).Name, 
                request);

            throw new EventlyException(typeof(TRequest).Name , null, exception);
        }

    }
}
