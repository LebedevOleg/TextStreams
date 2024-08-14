using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TextStreams.AppService.Contracts.Exceptions;

namespace TextStreams.Host;

public class ServiceExceptionFilter : IActionFilter, IOrderedFilter
{
    private readonly ILogger<ServiceExceptionFilter> _logger;

    public ServiceExceptionFilter(ILogger<ServiceExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            _logger.LogError(context.Exception, "Service exception");
            switch (context.Exception)
            {
                case DateValidateException:
                    context.Result = new BadRequestObjectResult(context.Exception.Message);
                    break;
                case StreamValidateException:
                    context.Result = new BadRequestObjectResult(context.Exception.Message);
                    break;
                default:
                    context.Result = new BadRequestObjectResult(context.Exception.Message);
                    break;
            }
        }

        context.ExceptionHandled = true;
    }

    public int Order => int.MaxValue - 10;
}