using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Mappers;

public static class OrderMapper
{
    public static OrderTable ToTable(this Order order)
    {
        return new OrderTable
        {
            Id = order.Id.Value,
            UserId = order.UserId.Value,
            Status = Enum.Parse<OrderStatusTable>(order.Status.ToString()),
            Quantity = order.Quantity,
            CreatedAt = order.CreatedAt
        };
    }

    public static Order ToDomain(this OrderTable table)
    {
        var status = Enum.Parse<OrderStatus>(table.Status.ToString());

        return Order.Reconstruct(
            OrderId.From(table.Id),
            UserId.From(table.UserId),
            table.Quantity,
            status,
            table.CreatedAt
        );
    }
}