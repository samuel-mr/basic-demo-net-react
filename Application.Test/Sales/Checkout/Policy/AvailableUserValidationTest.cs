using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using NSubstitute;

namespace Application.Test.Sales.Checkout.Policy;

public class AvailableUserValidationTest
{
    [Fact]
    public async Task When_TheUserExists_ShouldBe_Ok()
    {
        var VALID_USER = "BACH";
        var RANDOM_ORDER = 1;
        
        var item = new CheckoutPolicyItem(VALID_USER, RANDOM_ORDER);
        var orderRepository = Substitute.For<IUserRepository>();
        orderRepository
            .Exist(VALID_USER)
            .Returns(true);
        var policy = new AvailableUserValidation(orderRepository);

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task When_TheUserDoesntExists_ShouldBe_Failure()
    {
        var INVALID_USER = "xxx";
        var RANDOM_ORDER = 1;
        
        var item = new CheckoutPolicyItem(INVALID_USER, RANDOM_ORDER);
        var orderRepository = Substitute.For<IUserRepository>();
        orderRepository
            .Exist(INVALID_USER)
            .Returns(false);
        var policy = new AvailableUserValidation(orderRepository);

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsFailure);
    }
}