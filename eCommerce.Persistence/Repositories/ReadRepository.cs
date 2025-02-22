using System.Linq.Expressions;
using Domain.Entities.Common;
using eCommerce.Application.Interfaces;
using eCommerce.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace eCommerce.Infrastructure.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : class , IBaseEntity , new()
{
    private readonly AppDbContext context;

    public ReadRepository(AppDbContext context)
    {
        this.context = context;
    }
    
    
    private DbSet<T> Table => context.Set<T>();
    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,IIncludableQueryable<T,object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,bool enableTracking = false)
    {
        IQueryable<T> query = Table;
        if (!enableTracking) query = query.AsNoTracking();
        if(predicate is not null) query = query.Where(predicate);
        if(include is not null) query = include(query);
        if(orderBy is not null) return await orderBy(query).ToListAsync();
        
        return await query.ToListAsync();
    }

    public async Task<IList<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false, int page = 1, int pageSize = 10)
    {
        IQueryable<T> query = Table;
        if (!enableTracking) query = query.AsNoTracking();
        if(predicate is not null) query = query.Where(predicate);
        if(include is not null) query = include(query);
        if(orderBy is not null) return await orderBy(query.Skip((page-1)*pageSize).Take(pageSize)).ToListAsync();
        
        return await query.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
    {
        IQueryable<T> query = Table;
        if (!enableTracking) query = query.AsNoTracking();
        if(include is not null) query = include(query);
        return await query.FirstOrDefaultAsync(predicate);
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate,bool enableTracking = false)
    {
        if (enableTracking is false) Table.AsNoTracking();
        return  Table.Where(predicate);
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        Table.AsNoTracking();
        if (predicate is not null)  return await Table.Where(predicate).CountAsync();
        return await Table.CountAsync();
       
    }
}