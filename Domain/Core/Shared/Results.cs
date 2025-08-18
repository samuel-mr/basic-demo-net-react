using System.Diagnostics.CodeAnalysis;

namespace Domain.Core.Shared;

public record MyBaseResult
{
    internal MyBaseResult(bool isSuccess, Error? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }

    [MemberNotNullWhen(true, nameof(Error))]
    public bool IsFailure => !IsSuccess;

    public Error? Error { get; }

    public static MyBaseResult Success()
    {
        return new MyBaseResult(true, null);
    }

    public static MyBaseResult Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new MyBaseResult(false, error);
    }

    public static implicit operator MyBaseResult(Error error)
    {
        return Failure(error);
    }
}

public sealed record MyResult<T> : MyBaseResult
{
    private MyResult(bool isSuccess, T value, Error? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public T Value { get; }

    public static MyResult<T> Success(T value)
    {
        return new MyResult<T>(true, value, null);
    }

    public new static MyResult<T> Failure(Error error)
    {
        ArgumentNullException.ThrowIfNull(error);
        return new MyResult<T>(false, default, error);
    }

    public static implicit operator MyResult<T>(Error error)
    {
        return Failure(error);
    }

    public static implicit operator MyResult<T>(T value)
    {
        return Success(value);
    }
}