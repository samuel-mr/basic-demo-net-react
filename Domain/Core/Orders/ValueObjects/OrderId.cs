namespace Domain.Core.Orders.ValueObjects;

public record OrderId(Guid Value)
{
    public static OrderId New()
    {
        return new OrderId(Guid.NewGuid());
    }

    public static OrderId From(Guid value)
    {
        return new OrderId(value);
    }

    public static implicit operator Guid(OrderId orderId)
    {
        return orderId.Value;
    }

    public static implicit operator OrderId(Guid value)
    {
        return new OrderId(value);
    }
}