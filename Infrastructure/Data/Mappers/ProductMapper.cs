using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;
using Infrastructure.Data.Entities;

namespace Infrastructure.Data.Mappers;

public static class ProductMapper
{
    public static ProductTable ToTable(this Product product)
    {
        return new ProductTable
        {
            Id = product.Id.Value,
            Name = product.Name,
            StockQuantity = product.StockQuantity.Value,
            CreatedAt = product.CreatedAt
        };
    }

    public static Product ToDomain(this ProductTable entity)
    {
        return Product.Reconstruct(
            ProductId.From(entity.Id),
            entity.Name,
            StockQuantity.From(entity.StockQuantity),
            entity.CreatedAt
        );
    }
}