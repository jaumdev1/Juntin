using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Juntin.Infrastructure.Repository.JuntinMovieRepository;

public class JuntinMovieRepository : BaseRepository<JuntinMovie>, IJuntinMovieRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;
    public JuntinMovieRepository(ApplicationDbContext dbContext, IConfiguration configuration) : base(dbContext)
    {
        _context = dbContext;
        _configuration = configuration;
    }
}