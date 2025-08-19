namespace MyWebApp.Features.Sales.GetPurchasedOrders;

using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

public class GetPurchasedOrdersEndpoint : Endpoint<GetPurchasedOrdersRequest, Results<Ok<GetPurchasedOrdersResponse>, NotFound>>
{
    private readonly IOrderRepository _orderRepository;

    public GetPurchasedOrdersEndpoint(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public override void Configure()
    {
        Get("/api/orders/{userId}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Get orders by user";
            s.Description = "Retrieve all orders for a specific user";
        });
    }

    public override async Task<Results<Ok<GetPurchasedOrdersResponse>, NotFound>>
        ExecuteAsync(GetPurchasedOrdersRequest req, CancellationToken ct)
    {
        var userId = UserId.From(req.userId);
        var orders = await _orderRepository.GetOrdersByUserAsync(userId);

        if (!orders.Any()) return TypedResults.NotFound();

        var response = new GetPurchasedOrdersResponse
        {
            UserId = req.userId,
            Orders = orders.Select(o => new OrderDto
            {
                Id = o.Id.Value.ToString(),
                Quantity = o.Quantity.Value,
                Status = o.Status.ToString(),
                CreatedAt = o.CreatedAt
            }).ToList()
        };

        return TypedResults.Ok(response);
    }
}
