namespace Domain.Core.Shared;

public static class GenericErrors
{
    public static GenericError DomainError(string eMessage)
    {
        return new GenericError(
            "GENERIC_001",
            eMessage,
            ErrorCategory.Generic,
            ErrorSeverity.Error
        );
    }
}
public static class AuthErrors
{
    public static GenericError UnavailableUser(string user)
    {
        return new GenericError(
            "GENERIC_001",
            $"The use {user} is unknow.",
            ErrorCategory.Generic,
            ErrorSeverity.Error
        );
    }
}
public static class SalesErrors
{
    public static readonly SalesError InsufficientStock = new(
        "SALES_001",
        "Insufficient stock available for this order.",
        ErrorCategory.Sales,
        ErrorSeverity.Warning
    );

    public static readonly SalesError ProductNotFound = new(
        "SALES_002",
        "Product not found in inventory.",
        ErrorCategory.Sales,
        ErrorSeverity.Error
    );

    public static readonly SalesError OrderProcessingFailed = new(
        "SALES_003",
        "Failed to process order. Please try again later."
    );

    public static SalesError RateLimitExceeded(int retryAfter)
    {
        return new SalesError(
            "SALES_004",
            $"Rate limit of {retryAfter} per minute exceeded. Please wait before placing another order.",
            ErrorCategory.Sales,
            ErrorSeverity.Warning
        );
    }

    public static SalesError QuantityExceeded(int current, int expected)
    {
        return new SalesError(
            "SALES_005",
            $"Quantity limit of {expected} was exceeded, your current quantity is {current}.",
            ErrorCategory.Sales,
            ErrorSeverity.Warning
        );
    }

    
}