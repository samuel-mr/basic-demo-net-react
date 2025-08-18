namespace MyWebApp.Infra;

public static class ProblemDetailsServiceExtension
{
    /// <summary>
    ///     It must be use: app.UseExceptionHandler();
    /// </summary>
    /// <param name="sc"></param>
    public static void CustomProblemDetails(this IServiceCollection sc)
    {
        sc.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Instance =
                    $"{context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
                context.ProblemDetails.Extensions.Add("requestId", context.HttpContext.TraceIdentifier);
            };
        });
    }
}