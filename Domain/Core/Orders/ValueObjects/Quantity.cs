using Domain.Exceptions;

namespace Domain.Core.Orders.ValueObjects;

public record Quantity(int Value)
{
    // General rules, for explicit and dynamic rules see the policies into Application.Sales.Checkout.Policy namespace
    public const int MinAmount = 1;
    public const int MaxAmount = 1000;

    public static Quantity From(int value)
    {
        if (value < MinAmount)
            throw new DomainException($"Quantity must be at least {value}" );

        if (value > MaxAmount)
            throw new DomainException($"Quantity cannot exceed {value}" );

        return new Quantity(value);
    }

    public static implicit operator int(Quantity amount)
    {
        return amount.Value;
    }

    public static implicit operator Quantity(int value)
    {
        return From(value);
    }
}