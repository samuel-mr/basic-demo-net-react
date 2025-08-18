using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using FluentValidation;
using FluentValidation.Validators;
using MyWebApp.Extensions;

namespace MyWebApp.Features.Sales.ProcessOrder;

public class Validator : AbstractValidator<MyRequest>
{
    public Validator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Quantity).BeInRange(Quantity.MinAmount, Quantity.MaxAmount);
    }
}

