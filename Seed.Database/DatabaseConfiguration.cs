using Infrastructure.Data;
using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Seed.Database;

public static class DatabaseConfiguration
{
    public static async Task InitializeDatabaseAsync(OrderDbContext context)
    {
        // Ensure database is created
        await context.Database.EnsureCreatedAsync();

        // Seed initial data if needed
        await SeedInitialDataAsync(context);
    }

    private static async Task SeedInitialDataAsync(OrderDbContext context)
    {
        // Seed users if not exists
        if (!await context.Users.AnyAsync())
        {
            var users = new List<UserTable>
            {
                new()
                {
                    Id = "Bach",
                    Name = "Bach",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = "Pachelbel",
                    Name = "Pachelbel",
                    CreatedAt = DateTime.UtcNow
                },
                new()
                {
                    Id = "Mozart",
                    Name = "Mozart",
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Users.AddRangeAsync(users);
        }

        // Seed products if not exists
        if (!await context.Products.AnyAsync())
        {
            var products = new List<ProductTable>
            {
                new()
                {
                    Id = "corn-001",
                    Name = "Premium Corn",
                    StockQuantity = 5,
                    CreatedAt = DateTime.UtcNow
                }
            };

            await context.Products.AddRangeAsync(products);
        }

        await context.SaveChangesAsync();
    }
}