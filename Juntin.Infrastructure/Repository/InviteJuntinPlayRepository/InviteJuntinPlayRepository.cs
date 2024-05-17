using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.InviteJuntinPlayRepository;

public class InviteJuntinPlayRepository : BaseRepository<InviteJuntinPlay>, IInviteJuntinPlayRepository 
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    public InviteJuntinPlayRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }
    
    public async Task<InviteJuntinPlay> GetByCode(string code)
    {
        return await _context.Set<InviteJuntinPlay>().FirstOrDefaultAsync(x => x.Code == code);
    }
}