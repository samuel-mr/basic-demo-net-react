using Domain.Core.Orders.Entities;
using Domain.Core.Orders.Repositories;
using Domain.Core.Orders.ValueObjects;
using Infrastructure.Data.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class OrderRepository : IOrderRepository
{
    private readonly OrderDbContext _context;

    public OrderRepository(OrderDbContext context)
    {
        _context = context;
    }

    public async Task<Order?> GetByIdAsync(OrderId id)
    {
        var entity = await _context.Orders
            .FirstOrDefaultAsync(o => o.Id == id.Value);

        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserAsync(UserId userId)
    {
        var entities = await _context.Orders
            .Where(o => o.UserId == userId.Value)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();

        return entities.Select(e => e.ToDomain());
    }

    public async Task<int> GetCantOrdersByUserInTimeWindowAsync(UserId userId, DateTime since)
    {
        var entities = _context.Orders
            .Where(o => o.UserId == userId.Value && o.CreatedAt >= since);

        var sql = entities.ToQueryString();
        return await entities.CountAsync();
    }

    public async Task AddAsync(Order order)
    {
        var entity = order.ToTable();
        _context.Orders.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order order)
    {
        var entity = order.ToTable();
        _context.Orders.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(OrderId id)
    {
        var entity = await _context.Orders.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}