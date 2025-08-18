using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;

namespace Domain.Core.Orders.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(OrderId id);
    Task<IEnumerable<Order>> GetOrdersByUserAsync(UserId userId);
    Task<int> GetCantOrdersByUserInTimeWindowAsync(UserId userId);
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(OrderId id);
}