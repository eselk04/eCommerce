using Domain.Entities.Common;

namespace eCommerce.Application.Interfaces;

public interface IWriteRepository<T> where T : class , IBaseEntity , new()
{
     Task<T>  AddAsync(T entity);
    Task  AddRangeAsync(IList<T> entities);
    Task<T> UpdateAsync(T entity);
    Task UpdateRangeAsync(IList<T> entities);
    Task HardDeleteAsync(T entity);
    Task HardDeleteRangeAsync(IList<T> entities);
    
    
}