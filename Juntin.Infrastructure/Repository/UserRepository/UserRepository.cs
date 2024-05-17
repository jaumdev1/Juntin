using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.UserRepository;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await _context.Set<User>().FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<bool> UserExists(string email, string username)
    {
        return await _context.Set<User>().AnyAsync(x => x.Email == email || x.Username == username);
    }
}