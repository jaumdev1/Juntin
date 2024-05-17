using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Juntin.Infrastructure.Repository.UserViewedJuntinMovieRepository;

public class UserViewedJuntinMovieRepository : BaseRepository<UserViewedJuntinMovie>, IUserViewedJuntinMovieRepository
{
    private readonly ApplicationDbContext _context;
    public UserViewedJuntinMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
    
    
        public async Task<UserViewedJuntinMovie?> GetByJuntinUserAndJuntinMovie(Guid userJuntinId, Guid juntinMovieId)
    {
        return await _context.Set<UserViewedJuntinMovie>().Include(userViewedJuntinMovie => userViewedJuntinMovie.JuntinMovie)
            .FirstOrDefaultAsync(x => x.UserJuntinId == userJuntinId && x.JuntinMovieId == juntinMovieId);
    }

    public async Task<List<UserViewedJuntinMovie>> GetByJuntinUserAndJuntinPlay(Guid userJuntinId,
        Guid juntinPlayId)
    {
        return await _context.Set<UserViewedJuntinMovie>()
            .Where(x => x.UserJuntinId == userJuntinId && x.JuntinMovie.JuntinPlayId == juntinPlayId)
            .ToListAsync();
    }
    
    
}