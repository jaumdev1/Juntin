using System.Linq.Expressions;

namespace Domain.Contracts.Repository;

public interface IBaseRepository<T>
{
    Task<T?> GetById(Guid id);
    Task DeleteById(Guid id);
    Task Add(T entity);
    Task<T> Update(T entity);
    Task<T> UpdatePartialAsync<T>(T entity, params Expression<Func<T, object>>[] properties) where T : class;

}