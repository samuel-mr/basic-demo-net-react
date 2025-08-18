using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;

namespace Domain.Core.Orders.Repositories;

public interface IProductRepository
{
    Task<Product?> GetByIdAsync(ProductId id);
    Task<Product> UpdateStockAsync(Product product);
}