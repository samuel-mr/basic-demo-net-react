using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MyWebApp.Infra;

namespace MyWebApp.Helpers;

public static class InjectExternalResources
{
    public static void ConfigureDatabase(this IServiceCollection services)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseSqlite("Data Source=MySuperDatabase.db"));
    }

    public static void ConfigureExceptions(this IServiceCollection services)
    {
        services.CustomProblemDetails();
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
    public static void UseExceptions(this WebApplication app)
    {
        app.UseExceptionHandler();
        app.UseStatusCodePages();
    }
    
    public static void ConfigureApis(this IServiceCollection services)
    {
        services.AddFastEndpoints()
            .SwaggerDocument();

        services.AddCors();
    }
    public static void UseApis(this WebApplication app)
    {
        app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000", "https://localhost:3000"));
        app.UseFastEndpoints()
            .UseSwaggerGen();
    }
    
}