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
    
    
    private DbSet<T> Table => context.Set<T>();
    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,IIncludableQueryable<T,object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,bool enableTracking = false)
    {
        IQueryable<T> query = Table;
        if (!enableTracking) query = query.AsNoTracking();
        if(predicate is null) query = query.Where(predicate);
        if(orderBy is null) query = query.OrderBy(predicate);
        if(include is null) query = query.Include(predicate);
        
        return await query.ToListAsync();
    }

    public async Task<IList<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool enableTracking = false, int page = 1, int pageSize = 10)
    {
        IQueryable<T> query = Table;
        if (!enableTracking) query = query.AsNoTracking();
        if(predicate is null) query = query.Where(predicate);
        if(orderBy is null) query = query.OrderBy(predicate);
        if(include is null) query = query.Include(predicate);
        
        return await query.Skip(page*pageSize).Take(pageSize).ToListAsync();
    }

    public Task<IList<T>> GetAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        throw new NotImplementedException();
    }
}