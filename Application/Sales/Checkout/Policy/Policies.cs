using Application.Shared;
using Domain.Core.Orders.Repositories;
using Domain.Core.Shared;

namespace Application.Sales.Checkout.Policy;

public class MaximumItemsPerClientValidation : IValidation<CheckoutPolicyItem>
{
    private const int MAX_ALLOWED_ORDERS_PER_USER = 1;

    public Task<MyBaseResult> IsValid(CheckoutPolicyItem item)
    {
        if (item.Quantity > MAX_ALLOWED_ORDERS_PER_USER)
            return Task.FromResult(
                MyBaseResult.Failure(SalesErrors.QuantityExceeded(item.Quantity, MAX_ALLOWED_ORDERS_PER_USER)));
        return Task.FromResult(MyBaseResult.Success());
    }
}

public class RateLimiterForPurchaseValidation : IValidation<CheckoutPolicyItem>
{
    private const int MAX_ALLOWED_ORDERS_PER_USER = 1;
    private readonly IOrderRepository _orderRepository;

    public RateLimiterForPurchaseValidation(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<MyBaseResult> IsValid(CheckoutPolicyItem item)
    {
        var orders =
            await _orderRepository.GetCantOrdersByUserInTimeWindowAsync(item.UserId);
        if (orders >= MAX_ALLOWED_ORDERS_PER_USER)
            return MyBaseResult.Failure(SalesErrors.RateLimitExceeded(MAX_ALLOWED_ORDERS_PER_USER));

        return MyBaseResult.Success();
    }
}

public class AvailableUserValidation : IValidation<CheckoutPolicyItem>
{
    private readonly IUserRepository _userRepository;

    public AvailableUserValidation(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<MyBaseResult> IsValid(CheckoutPolicyItem item)
    {
       if(!await _userRepository.Exist(item.UserId))
           return MyBaseResult.Failure(AuthErrors.UnavailableUser(item.UserId));
       return MyBaseResult.Success();
    }
}