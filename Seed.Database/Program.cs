using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Seed.Database;

try
{
    var dbPath = Path.Combine("..", "..", "..", "..", "MyWebApp", "MySuperDatabase.db");

    var options = new DbContextOptionsBuilder<OrderDbContext>()
        .UseSqlite($"Data Source={dbPath}")
        .Options;

    var context = new OrderDbContext(options);
    await DatabaseConfiguration.InitializeDatabaseAsync(context);

    Console.WriteLine("DB seeded correctly");
}
catch (Exception e)
{
    Console.WriteLine(e);
}