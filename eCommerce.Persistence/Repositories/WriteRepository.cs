using Domain.Entities.Common;
using eCommerce.Application.Interfaces;
using eCommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : class , IBaseEntity , new()
{
    private readonly AppDbContext context;

    public WriteRepository(AppDbContext _context)
    {
        this.context = _context;
    }
    DbSet<T> Table => context.Set<T>();
    public async Task<T> AddAsync(T entity)
    { 
        await Table.AddAsync(entity);
        return entity;
    }

    public async Task AddRangeAsync(IList<T> entities)
    {
        await Table.AddRangeAsync(entities);
    
    }

    public async Task<T> UpdateAsync(T entity)
    {
       await Task.Run(()=>Table.Update(entity));
        return entity;
    }

    public async Task UpdateRangeAsync(IList<T> entities)
    {
        await Task.Run(()=>Table.UpdateRange(entities));
        
    }

    public async Task HardDeleteAsync(T entity)
    {
        await Task.Run(() => Table.Remove(entity));

    }
}