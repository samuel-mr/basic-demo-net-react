using Domain.Exceptions;

namespace Domain.Core.Orders.ValueObjects;

public record StockQuantity(int Value)
{
    private const int MinQuantity = 0;
    private const int MaxQuantity = 10000;

    public static StockQuantity From(int value)
    {
        if (value < MinQuantity)
            throw new DomainException($"Stock quantity '{value}' cannot be less than {MinQuantity}");

        if (value > MaxQuantity)
            throw new DomainException($"Stock quantity '{value}' cannot exceed {MaxQuantity}");

        return new StockQuantity(value);
    }

    public static implicit operator int(StockQuantity quantity)
    {
        return quantity.Value;
    }
    public static implicit operator StockQuantity(int value)
    {
        return From(value);
    }

    public StockQuantity Reduce(int amount)
    {
        if (amount <= 0)
            throw new DomainException("Reduction amount must be positive");

        if (Value < amount)
            throw new DomainException($"Insufficient stock. Available: {Value}, Requested: {amount}");

        return new StockQuantity(Value - amount);
    }
}