using System.Collections.Immutable;

namespace Domain.Core.Shared;

public enum ErrorCategory
{
    Generic,
    Sales
}

public enum ErrorSeverity
{
    Info,
    Warning,
    Error,
    Critical
}

public abstract record Error
{
    protected Error(
        string code,
        string message,
        ErrorCategory category,
        ErrorSeverity severity = ErrorSeverity.Error)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Message = message ?? string.Empty;
        Category = category;
        Severity = severity;
    }

    public string Code { get; }
    public string Message { get; }
    public ErrorCategory Category { get; }
    public ErrorSeverity Severity { get; }

    public override string ToString()
    {
        return $"{Code}: {Message}";
    }
}

public sealed record GenericError : Error
{
    public GenericError(
        string code,
        string message,
        ErrorCategory category = ErrorCategory.Generic,
        ErrorSeverity severity = ErrorSeverity.Error)
        : base(code, message, category, severity)
    {
    }
}

public sealed record SalesError : Error
{
    public SalesError(
        string code,
        string message,
        ErrorCategory category = ErrorCategory.Sales,
        ErrorSeverity severity = ErrorSeverity.Error)
        : base(code, message, category, severity)
    {
    }

}