using Application.Shared;

namespace Application.Sales.Checkout.Policy;

public record CheckoutPolicyItem(string UserId, int Quantity);

public class CheckoutPolicy : ValidationPolicy<CheckoutPolicyItem>
{
    public CheckoutPolicy(RateLimiterForPurchaseValidation rateLimiter,
        MaximumItemsPerClientValidation maximumItemsPerClient,
        AvailableUserValidation  availableUserValidation)
    {
        Validations.Add(rateLimiter);
        Validations.Add(maximumItemsPerClient);
        // just for demonstration purpose in sake of completeness
        Validations.Add(availableUserValidation);
    }
}