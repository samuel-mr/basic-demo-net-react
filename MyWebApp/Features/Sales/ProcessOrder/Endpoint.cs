using Application.Sales.Checkout;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace MyWebApp.Features.Sales.ProcessOrder;

public class ProcessOrderEndpoint : Endpoint<
    MyRequest,
    Results<Ok<string>, ProblemDetails, InternalServerError<string>>>
{
    private readonly OrderProcessingService _orderHandler;

    public ProcessOrderEndpoint(OrderProcessingService orderHandler)
    {
        _orderHandler = orderHandler;
    }

    public override void Configure()
    {
        Post("/api/order/create");
        AllowAnonymous();
        Validator<Validator>();
        Summary(s =>
        {
            s.Summary = "Process order";
            s.Description =
                "Process the purchase of corn from the store, validating business rules including minimum amount, rate limits, and business hours";
        });
    }

    public override async Task<Results<Ok<string>, ProblemDetails, InternalServerError<string>>>
        ExecuteAsync(MyRequest req, CancellationToken ct)
    {
        var result = await _orderHandler.ProcessOrderAsync(req.UserId, req.Quantity);

        if (result.IsFailure) return TypedResults.InternalServerError(result.Error.Message);
        return TypedResults.Ok(result.Value.CreatedOrderId.ToString());
    }
}