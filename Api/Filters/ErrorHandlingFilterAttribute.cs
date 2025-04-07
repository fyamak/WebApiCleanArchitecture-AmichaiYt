using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        // We can use filter as attribute [...] or
        // we can define it in program.cs builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
        var exception = context.Exception;
        context.Result = new ObjectResult(new
        {
            error = "An unexpected error occurred. (filter attribute)",
            details = exception.Message,
        })
        {
            StatusCode = 500
        };
        context.ExceptionHandled = true;
    }
}
