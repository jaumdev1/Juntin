using System.Linq.Expressions;
using Domain.Contracts.Repository;
using Domain.Entities;
using Juntin.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Juntin.Infrastructure.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    #region Properties

    private readonly ApplicationDbContext _dbContext;
    private IBaseRepository<T> _baseRepositoryImplementation;

    #endregion Properties

    #region Constructors

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #endregion Constructors

    public async Task Add(T entity)
    {
     
        _dbContext
            .Set<T>()
            .Add(entity);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteById(Guid id)
    {
        var entity = await _dbContext
            .Set<T>()
            .FindAsync(id);

        if (entity == null) return;

        entity.IsDeleted = true;

        await _dbContext.SaveChangesAsync();
    }

    public async Task<T?> GetById(Guid id)
    {
        return await _dbContext
            .Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> Update(T entity)
    {
      
        _dbContext
            .Set<T>()
            .Update(entity);

        await _dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<T> UpdatePartialAsync<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class
    {
        _dbContext.Attach(entity);
        foreach (var property in properties)
        {
            _dbContext.Entry(entity).Property(property).IsModified = true;
        }
        await _dbContext.SaveChangesAsync();

        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

        return entity;
    }
    
}