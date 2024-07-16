using internPlatform.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Infrastructure.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        Task<T> GetById(int Id, string includeProperties = null);

        Task<PaginatedList<T>> GetPaginatedAsync(PaginationOptions options, Expression<Func<T, int>> orderBy);

        T Add(T entity);
        T Remove(T entity);
        void RemoveRange(IEnumerable<T> entityRange);
        void Update(T entity);
        Task<int> Save();



    }
}
