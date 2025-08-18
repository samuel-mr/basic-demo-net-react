using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;

namespace Domain.Test.Core.Orders.Entities;

public class ProductTest
{
    [Fact]
    public void When_QuantityIsLessThanCurrentStock_ShouldBe_Ok()
    {
        var STOCK = 5;
        var QUANTITY_REQUIRED = 2;
        var product = Product.Reconstruct(ProductId.From("corn-001"), "Corn", STOCK, DateTime.UtcNow);
        
        Assert.True(product.HasSufficientStock(QUANTITY_REQUIRED));
    }
    
    [Fact]
    public void When_QuantityIsMoreThanCurrentStock_ShouldBe_Insuficient()
    {
        var STOCK = 5;
        var QUANTITY_REQUIRED = 10;
        var product = Product.Reconstruct(ProductId.From("corn-001"), "Corn", STOCK, DateTime.UtcNow);
        
        Assert.False(product.HasSufficientStock(QUANTITY_REQUIRED));
    }
    
    [Fact]
    public void When_ReduceStock_ShouldBe_RefectInTheFinalProperty()
    {
        var STOCK = 5;
        var QUANTITY_REQUIRED =1;
        var product = Product.Reconstruct(ProductId.From("corn-001"), "Corn", STOCK, DateTime.UtcNow);

        product.ReduceStock(QUANTITY_REQUIRED);
            
        Assert.Equal(product.StockQuantity.Value, 4 );
    }
}