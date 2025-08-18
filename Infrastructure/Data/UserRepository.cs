using Domain.Core.Orders.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class UserRepository : IUserRepository
{
    private readonly OrderDbContext _db;

    public UserRepository(OrderDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Exist(string userId)
    {
        var ss = await _db.Users.AnyAsync(u => u.Id == userId);
        return ss;
    }
}