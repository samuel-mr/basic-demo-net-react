using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MyWebApp.Features.Sales.GetOrders;

public class GetOrdersEndpoint : Endpoint<GetOrdersRequest, Results<Ok<GetOrdersResponse>, NotFound>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrdersEndpoint(IOrderRepository orderRepository)
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


    public override async Task<Results<Ok<GetOrdersResponse>, NotFound>>
        ExecuteAsync(GetOrdersRequest req, CancellationToken ct)
    {
        var userId = UserId.From(req.UserId);
        var orders = await _orderRepository.GetOrdersByUserAsync(userId);

        if (!orders.Any()) return TypedResults.NotFound();

        var response = new GetOrdersResponse
        {
            UserId = req.UserId,
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

public class GetOrdersRequest
{
    public string UserId { get; set; } = string.Empty;
}

public class GetOrdersResponse
{
    public string UserId { get; set; } = string.Empty;
    public List<OrderDto> Orders { get; set; } = new();
}

public class OrderDto
{
    public string Id { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}