using Domain.Entities.Common;

namespace eCommerce.Application.Interfaces.UnitOfWorks;

public interface IUnitOfWork : IAsyncDisposable
{
    IReadRepository<TEntity> GetReadRepository<TEntity>() where TEntity : class, IBaseEntity, new();
    IWriteRepository<TEntity> GetWriteRepository<TEntity>() where TEntity : class, IBaseEntity, new();
    Task<int> SaveAsync();
    int Save();
}