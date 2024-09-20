using FrontEndWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace FrontEndWebAPI.Filters;

public class UnhandledExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger<UnhandledExceptionFilter> _logger;

    public UnhandledExceptionFilter(ILogger<UnhandledExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        var response = new ApiResponse(context.Exception, $"Error occured while processing {context.HttpContext.Request.Path}");
        _logger.LogError(context.Exception, $"ERROR: {response.GetErrors()} URL:{context.HttpContext.Request.Path}");

        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError
        };
    }
}

