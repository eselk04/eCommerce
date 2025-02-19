using System.Linq.Expressions;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace eCommerce.Application.Interfaces;

public interface IReadRepository<T> where T : class , IBaseEntity ,new()
{
    public Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        , Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false);

    public Task<IList<T>> GetAllAsyncByPaging(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        , Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false, int page = 1,
        int pageSize = 10);
    
    public Task<IList<T>> GetAsync(Expression<Func<T, bool>>? predicate,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null
        , Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool enableTracking = false);
    
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);
}