using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MyWebApp;

/// <summary>
///     Global handler for incoming exceptions. It must be use app.UseExceptionHandler(); app.UseStatusCodePages();
/// </summary>
/// <param name="pds"></param>
public class GlobalExceptionHandler(IProblemDetailsService pds) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
        return await pds.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Type = exception.GetType().Name,
                Title = "Ocurrió un error interno en mi super server", // TODO: be specific
                Detail = exception.Message
            }
        });
    }
}