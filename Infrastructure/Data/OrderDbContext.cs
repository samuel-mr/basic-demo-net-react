using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    public DbSet<OrderTable> Orders { get; set; }
    public DbSet<UserTable> Users { get; set; }
    public DbSet<ProductTable> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure OrderEntity
        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired();

            entity.Property(e => e.UserId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Quantity)
                .IsRequired();

            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // Configure UserEntity
        modelBuilder.Entity<UserTable>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // Configure relationships
        modelBuilder.Entity<OrderTable>()
            .HasOne<UserTable>()
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure ProductEntity
        modelBuilder.Entity<ProductTable>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(e => e.StockQuantity)
                .IsRequired();

            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });
    }
}