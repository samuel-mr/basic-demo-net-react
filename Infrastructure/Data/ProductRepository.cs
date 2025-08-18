using Ardalis.GuardClauses;
using Domain.Core.Orders.Entities;
using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using Infrastructure.Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository : IProductRepository
{
    private readonly OrderDbContext _context;

    public ProductRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(ProductId id)
    {
        var entity = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id.Value);

        return entity?.ToDomain();
    }

    public async Task<Product> UpdateStockAsync(Product product)
    {
        var existingEntity = await _context.Products.FindAsync(product.Id.Value);
        Guard.Against.Null(existingEntity, message: $"Product with ID {product.Id.Value} not found");

        existingEntity.StockQuantity = product.StockQuantity.Value;
        var itemsUpdated = await _context.SaveChangesAsync();
        if (itemsUpdated != 1)
            throw new InvalidOperationException($"Product with ID {product.Id.Value} updated {itemsUpdated} items");
        return existingEntity.ToDomain();
    }
}