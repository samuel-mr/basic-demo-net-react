using Application.Sales.Checkout;
using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Repositories;
using Infrastructure.Data;

namespace MyWebApp.Helpers;

public static class InjectRepository
{
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    public static void RegisterProcess(this IServiceCollection services)
    {
        services.AddScoped<OrderProcessingService>();
    }

    public static void RegisterValidationPolicies(this IServiceCollection services)
    {
        services.AddScoped<RateLimiterForPurchaseValidation>();
        services.AddScoped<MaximumItemsPerClientValidation>();
        services.AddScoped<AvailableUserValidation>();
    }

    public static void RegisterPolicies(this IServiceCollection services)
    {
        services.AddScoped<CheckoutPolicy>();
    }
}