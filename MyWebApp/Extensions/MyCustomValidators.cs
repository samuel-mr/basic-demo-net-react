using FluentValidation;

namespace MyWebApp.Extensions;

public static class MyCustomValidators
{
    public static IRuleBuilderOptions<T, TElement> BeInRange<T, TElement>
        (this IRuleBuilder<T, TElement> ruleBuilder, int min, int max)
        where TElement : struct, IComparable<int>
    {
        return ruleBuilder
            .Must(list => list.CompareTo(min) >= 0 && list.CompareTo(max) <= 0)
            .WithMessage("{PropertyName} must be between " + min + " and " + max);
    }
}