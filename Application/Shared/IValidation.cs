using Domain.Core.Shared;

namespace Application.Shared;

public interface IValidation<T>
{
    Task<MyBaseResult> IsValid(T item);
}