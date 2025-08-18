using Domain.Core.Orders.ValueObjects;
using Domain.Core.Shared;

namespace Domain.Core.Orders.Entities;

public class Order : Entity<OrderId>
{
    private Order()
    {
    } // For EF Core

    private Order(OrderId id, UserId userId, Quantity quantity) : base(id)
    {
        UserId = userId;
        Quantity = quantity;
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
    }

    private Order(OrderId id, UserId userId, Quantity quantity, OrderStatus status, DateTime createdAt) : base(id)
    {
        UserId = userId;
        Quantity = quantity;
        Status = status;
        CreatedAt = createdAt;
    }

    public UserId UserId { get; private set; }
    public Quantity Quantity { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public static Order Create(UserId userId, Quantity quantity)
    {
        return new Order(OrderId.New(), userId, quantity);
    }

    public static Order Reconstruct(OrderId id, UserId userId, int quantity, OrderStatus status, DateTime createdAt)
    {
        return new Order(id, userId, quantity, status, createdAt);
    }
}