using Application.Sales.Checkout.Policy;
using Domain.Core.Orders.Entities;
using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using Domain.Core.Shared;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;

namespace Application.Sales.Checkout;

/// <summary>
///     Logic to process the order of each customer
/// </summary>
public class OrderProcessingService
{
    private readonly CheckoutPolicy _checkoutPolicy;
    private readonly OrderDbContext _context;
    private readonly ILogger<OrderProcessingService> _logger;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderProcessingService(
        OrderDbContext context,
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        CheckoutPolicy checkoutPolicy,
        ILogger<OrderProcessingService> logger)
    {
        _context = context;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _checkoutPolicy = checkoutPolicy;
        _logger = logger;
    }

    public async Task<MyResult<OrderResult>> ProcessOrderAsync(string userId, int quantity)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            _logger.LogInformation("Starting order processing for user {UserId} with amount {Amount}", userId,
                quantity);

            var userIdValue = UserId.From(userId);
            var quantityValue = Quantity.From(quantity);

            // DDD logic for purchase - Domain business rules validationssss
            var policyValidation = await _checkoutPolicy.ValidateAsync(
                new CheckoutPolicyItem(userId, quantityValue));

            if (policyValidation.IsFailure)
            {
                _logger.LogWarning("Order validation failed for user {UserId}: {Error}", userId,
                    policyValidation.Error.Message);
                return policyValidation.Error;
            }

            // Stock
            var defaultProductId = ProductId.From("corn-001"); // Default product for Bob's store
            var product = await _productRepository.GetByIdAsync(defaultProductId);

            if (product == null)
            {
                _logger.LogError("Product {ProductId} not found in inventory", defaultProductId.Value);
                return SalesErrors.ProductNotFound;
            }

            if (!product.HasSufficientStock(quantityValue.Value))
            {
                _logger.LogWarning(
                    "Insufficient stock for product {ProductId}. Available: {Available}, Requested: {Requested}",
                    product.Id.Value, product.StockQuantity.Value, quantityValue.Value);
                return SalesErrors.InsufficientStock;
            }

            product.ReduceStock(quantityValue);
            await _productRepository.UpdateStockAsync(product);

            // Order
            var order = Order.Create(userIdValue, quantityValue);
            await _orderRepository.AddAsync(order);

            await transaction.CommitAsync();

            _logger.LogInformation(
                "Order {OrderId} successfully processed for user {UserId} with amount {Amount}. Stock reduced by {Reduction}",
                order.Id.Value, userId, quantity, quantityValue.Value);

            return MyResult<OrderResult>.Success(new OrderResult(order.Id));
        }
        catch (DomainException e)
        {
            await transaction.RollbackAsync();
            _logger.LogError(e, "Domain exception occured");
            return GenericErrors.DomainError(e.Message);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogWarning("Transaction rolled back for user {UserId}", userId);
            _logger.LogError(ex,
                "Unexpected error occurred while processing order for user {UserId} with amount {Amount}", userId,
                quantity);
            return SalesErrors.OrderProcessingFailed;
        }
    }
}

public record OrderResult(Guid CreatedOrderId);