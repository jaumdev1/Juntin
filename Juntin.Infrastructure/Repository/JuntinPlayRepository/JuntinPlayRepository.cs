using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.JuntinRepository;

public class JuntinPlayRepository : BaseRepository<JuntinPlay>, IJuntinPlayRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public JuntinPlayRepository(ApplicationDbContext dbContext,   IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }
    
    public async Task<List<JuntinPlay>> GetPage(int page, Guid OwnerId)
    {
        var pageSize = 10;// Int32.Parse(_configuration.GetSection("PageSize").GetSection("DefaultPageSize").Value);
      return await _context.Set<UserJuntin>()
            .Where(x => x.UserId == OwnerId || x.JuntinPlay.OwnerId == OwnerId)
            .Select(x => x.JuntinPlay)
            .Skip(page * pageSize)
            .Take(pageSize)
            .ToListAsync();
       
        
    }
}