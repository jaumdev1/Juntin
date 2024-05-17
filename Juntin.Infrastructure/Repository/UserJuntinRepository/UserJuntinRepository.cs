using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.UserJuntinRepository;

public class UserJuntinRepository : BaseRepository<UserJuntin>, IUserJuntinRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public UserJuntinRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
       
    }

    public async Task<bool> IsUserJuntin(Guid JuntinPlayId, Guid userId)
    {
        var userJuntins = _context.Set<UserJuntin>()
            .Where(x => x.UserId == userId && x.JuntinPlayId == JuntinPlayId)
            .Select(x => new { x.UserId, x.JuntinPlayId });

        var adminJuntins = _context.Set<AdminJuntin>()
            .Where(x => x.UserId == userId && x.JuntinPlayId == JuntinPlayId)
            .Select(x => new { x.UserId, x.JuntinPlayId });
        
        var combinedQuery = userJuntins.Concat(adminJuntins);
        
        var anyUserOrAdmin = await combinedQuery.AnyAsync();
        return anyUserOrAdmin;
    }

    public Task<UserJuntin> GetByJuntinPlayAndUser(Guid JuntinPlayId, Guid userId)
    {
        return _context.Set<UserJuntin>()
            .FirstOrDefaultAsync(x => x.UserId == userId && x.JuntinPlayId == JuntinPlayId);
    }
}