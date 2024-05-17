using Domain.Contracts.Repository;
using Domain.Dtos.JuntinPlay;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.JuntinRepository;

public class JuntinPlayRepository : BaseRepository<JuntinPlay>, IJuntinPlayRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public JuntinPlayRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }

    public async Task<List<JuntinPlayResult>> GetPage(int page, Guid OwnerId)
    {
        var pageSize = int.Parse(_configuration.GetSection("Paging").GetSection("DefaultPageSize").Value);
        var juntinPlays = await _context.Set<JuntinPlay>()
            .Include(jp => jp.UserJuntins)
            .Include(jp => jp.JuntinMovies)
            .Where(jp => jp.UserJuntins.Any(uj => uj.UserId == OwnerId))
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return juntinPlays.Select(s =>
            new JuntinPlayResult(
                s.Id,
                s.Name,
                s.Category,
                s.Color,
                s.TextColor,
                s.UserJuntins.Count,
                s.JuntinMovies.Count
            )).ToList();
    }
    
    public async Task<JuntinPlay> GetByIdAndListUserJuntin(Guid Id)
    {
        return await _context.Set<JuntinPlay>()
            .Include(jp => jp.UserJuntins)
            .Include(jp => jp.JuntinMovies.Where(jm => !jm.IsWatchedEveryone))
            .FirstOrDefaultAsync(jp => jp.Id == Id);
    }

    public async Task<JuntinPlayResult?> GetByIdAndUserAndMovieCount(Guid Id)
    {
        return await _context.Set<JuntinPlay>()
            .Include(jp => jp.UserJuntins)
            .Include(jp => jp.JuntinMovies).Where(jp => jp.Id == Id).Select(s =>
            new JuntinPlayResult(
                s.Id,
                s.Name,
                s.Category,
                s.Color,
                s.TextColor,
                s.UserJuntins.Count,
                s.JuntinMovies.Count
            )).FirstOrDefaultAsync();  
    }
}