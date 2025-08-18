using Domain.Core.Orders.ValueObjects;
using Domain.Core.Shared;

namespace Domain.Core.Orders.Entities;

public class Product : Entity<ProductId>
{
    private Product()
    {
    } // For EF Core

    public Product(ProductId id, string name, StockQuantity stockQuantity) : base(id)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        StockQuantity = stockQuantity;
        CreatedAt = DateTime.UtcNow;
    }

    // Internal constructor for EF Core reconstruction
    internal Product(ProductId id, string name, StockQuantity stockQuantity, DateTime createdAt) : base(id)
    {
        Name = name;
        StockQuantity = stockQuantity;
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

    // Internal method for EF Core to reconstruct the entity
    public static Product Reconstruct(ProductId id, string name, StockQuantity stockQuantity, DateTime createdAt)
    {
        return new Product(id, name, stockQuantity, createdAt);
    }
}