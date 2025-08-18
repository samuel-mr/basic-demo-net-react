using Domain.Exceptions;

namespace Domain.Core.Orders.ValueObjects;

public record ProductId(string Value)
{
    public static ProductId From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Product ID cannot be null or empty");

        return new ProductId(value);
    }

    public static implicit operator string(ProductId productId)
    {
        return productId.Value;
    }

    public static implicit operator ProductId(string value)
    {
        return From(value);
    }
}