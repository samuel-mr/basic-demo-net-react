using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using NSubstitute;

namespace Application.Test.Sales.Checkout.Policy;

public class RateLimiterForPurchaseValidationTest
{
    [Fact]
    public async Task When_YouHavePreviousOrdersIntoTheTimewindow_ShouldBe_Unsucess()
    {
        var RANDOM_USER = "user";
        var RANDOM_ORDER = 1;
        var EXISTING_ORDER = 1;
        
        var item = new CheckoutPolicyItem(RANDOM_USER, RANDOM_ORDER);
        var orderRepository = Substitute.For<IOrderRepository>();
        orderRepository
            .GetCantOrdersByUserInTimeWindowAsync(RANDOM_USER, Arg.Any<DateTime>())
            .Returns(EXISTING_ORDER);
        var policy = new RateLimiterForPurchaseValidation(orderRepository);

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsFailure);
    }
    
    [Fact]
    public async Task When_DoesntHavePreviousOrders_ShouldBe_Ok()
    {
        var RANDOM_USER = "user";
        var RANDOM_ORDER = 1;
        var NON_EXISTING_ORDER = 0;
        
        var item = new CheckoutPolicyItem(RANDOM_USER, RANDOM_ORDER);
        var orderRepository = Substitute.For<IOrderRepository>();
        orderRepository
            .GetCantOrdersByUserInTimeWindowAsync("UserId", DateTime.Now)
            .Returns(NON_EXISTING_ORDER);
        var policy = new RateLimiterForPurchaseValidation(orderRepository);

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsSuccess);
    }
}