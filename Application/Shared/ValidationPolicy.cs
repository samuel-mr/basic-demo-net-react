using Domain.Core.Shared;

namespace Application.Shared;

public abstract class ValidationPolicy<T>
{
    protected List<IValidation<T>> Validations { get; } = new();

    public async Task<MyBaseResult> ValidateAsync(T item)
    {
        foreach (var validation in Validations)
        {
            var result = await validation.IsValid(item);
            if (result.IsFailure)
                return result;
        }

        return MyBaseResult.Success();
    }
}