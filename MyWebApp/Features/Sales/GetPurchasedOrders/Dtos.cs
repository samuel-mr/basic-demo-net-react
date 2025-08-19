namespace MyWebApp.Features.Sales.GetPurchasedOrders;

public record GetPurchasedOrdersRequest
{
    public string userId { get; set; } = string.Empty;
}

public class GetPurchasedOrdersResponse
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