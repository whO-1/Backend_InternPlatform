using internPlatform.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Infrastructure.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        Task<T> GetById(int Id, string includeProperties = null);

        Task<PaginatedList<T>> GetPaginatedAsync(IQueryable<T> query, PaginationOptions options);

        T Add(T entity);
        T Remove(T entity);
        void RemoveRange(IEnumerable<T> entityRange);
        void Update(T entity);
        Task<int> Save();



    }
}
