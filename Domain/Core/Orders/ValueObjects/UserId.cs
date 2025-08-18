using Ardalis.GuardClauses;
using Domain.Exceptions;

namespace Domain.Core.Orders.ValueObjects;

public record UserId(string Value)
{
    public static UserId From(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("UserId cannot be null or empty");
        return new UserId(value);
    }

    public static implicit operator string(UserId userId)
    {
        return userId.Value;
    }

    public static implicit operator UserId(string value)
    {
        return From(value);
    }
}