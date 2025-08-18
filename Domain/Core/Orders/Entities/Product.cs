using Domain.Core.Orders.ValueObjects;
using Domain.Core.Shared;

namespace Domain.Core.Orders.Entities;

public class Product : Entity<ProductId>
{
    private Product()
    {
    } // For EF Core

    private Product(ProductId id, string name, StockQuantity stockQuantity) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        StockQuantity = stockQuantity;
        CreatedAt = DateTime.UtcNow;
    }

    private Product(ProductId id, string name, StockQuantity stockQuantity, DateTime createdAt) : 
        this (id,name, stockQuantity)
    {
        CreatedAt = createdAt;
    }

    public string Name { get; private set; }
    public StockQuantity StockQuantity { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public void ReduceStock(int amount)
    {
        StockQuantity = StockQuantity.Reduce(amount);
    }

    public bool HasSufficientStock(int amount)
    {
        return StockQuantity.Value >= amount;
    }
    
    public static Product Reconstruct(ProductId id, string name, StockQuantity stockQuantity, DateTime createdAt)
    {
        return new Product(id, name, stockQuantity, createdAt);
    }
}