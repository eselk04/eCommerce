using Domain.Entities.Common;
using eCommerce.Application.Interfaces;
using eCommerce.Application.Interfaces.UnitOfWorks;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Persistence.Context;

namespace eCommerce.Persistence.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{ 
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }
    public async ValueTask DisposeAsync() => await _context.DisposeAsync();

     IReadRepository<TEntity> IUnitOfWork.GetReadRepository<TEntity>() =>
        new ReadRepository<TEntity>(_context);

     IWriteRepository<TEntity> IUnitOfWork.GetWriteRepository<TEntity>() => new WriteRepository<TEntity>(_context);

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    public int Save() => _context.SaveChanges();
}