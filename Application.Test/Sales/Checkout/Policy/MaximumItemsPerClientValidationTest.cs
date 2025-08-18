using Application.Sales.Checkout.Policy;

namespace Application.Test.Sales.Checkout.Policy;

public class MaximumItemsPerClientValidationTest
{
    [Fact]
    public async Task When_OrderHasMeetRequirements_ShouldBeOk()
    {
        var VALID_QUANTITY_ORDER =  MaximumItemsPerClientValidation.MAX_ALLOWED_ORDERS_PER_USER;
        var RANDOM_USER = "user";
        var item = new CheckoutPolicyItem(RANDOM_USER, VALID_QUANTITY_ORDER);
        var policy = new MaximumItemsPerClientValidation();

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsSuccess);
    }
    
    [Fact]
    public async Task When_OrderExceedTheQuantity_ShouldBeFase()
    {
        var VALID_QUANTITY_ORDER =  MaximumItemsPerClientValidation.MAX_ALLOWED_ORDERS_PER_USER + 1;
        var RANDOM_USER = "user";
        var item = new CheckoutPolicyItem(RANDOM_USER, VALID_QUANTITY_ORDER);
        var policy = new MaximumItemsPerClientValidation();

        var result = await policy.IsValid(item);
        
        Assert.True(result.IsFailure);
    }
}