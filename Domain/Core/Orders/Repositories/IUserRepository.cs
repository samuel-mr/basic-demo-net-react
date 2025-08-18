namespace Domain.Core.Orders.Repositories;

public interface IUserRepository
{
    Task<bool> Exist(string userId);
}

