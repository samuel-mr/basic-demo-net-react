using Domain.Core.Orders.Entities;
using Domain.Core.Orders.ValueObjects;

namespace Domain.Core.Orders.Repositories;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetOrdersByUserAsync(UserId userId);
    Task<int> GetCantOrdersByUserInTimeWindowAsync(UserId userId, DateTime since);
    Task AddAsync(Order order);
}