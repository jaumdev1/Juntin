using Domain.Contracts.Repository;
using Domain.Dtos.JuntinMovie;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.JuntinMovieRepository;

public class JuntinMovieRepository : BaseRepository<JuntinMovie>, IJuntinMovieRepository
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;

    public JuntinMovieRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }

    public async Task<List<ResultJuntinMovieDto>> GetPage(int page, Guid juntinPlayId)
    {
        var pageSize = int.Parse(_configuration.GetSection("Paging").GetSection("DefaultPageSize").Value);
        return await _context.Set<JuntinMovie>()
            .Where(x => x.JuntinPlayId == juntinPlayId && x.IsWatchedEveryone == false) 
            .Skip(page)
            .Take(pageSize)
            .Select(s=>
                new ResultJuntinMovieDto
                {
                    Id = s.Id, Title = s.Title, JuntinPlayId = s.JuntinPlayId, UserName = s.User.Username,
                    Description = s.Description, TmdbId = s.TmdbId, UrlImage = s.UrlImage,
                    
                } )   
            .ToListAsync();
    }
    public async Task<List<ResultHistoricJuntinMovieDto>> GetHistoric(int page, Guid juntinPlayId)
    {
        var pageSize = int.Parse(_configuration.GetSection("Paging").GetSection("DefaultPageSize").Value);
        return await _context.Set<JuntinMovie>()
            .Where(x => x.JuntinPlayId == juntinPlayId && x.IsWatchedEveryone == true) 
            .Skip(page)
            .Take(pageSize)
            .Select(s=>
                new ResultHistoricJuntinMovieDto
                {
                    Id = s.Id, Title = s.Title, JuntinPlayId = s.JuntinPlayId, UserName = s.User.Username,
                    Description = s.Description, TmdbId = s.TmdbId, UrlImage = s.UrlImage,
                    
                } )   
            .ToListAsync();
    }
}