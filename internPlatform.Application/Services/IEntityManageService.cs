using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IEntityManageService<T> where T : class
    {
        Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        T Add(T entity);
        void Update(T entity);

        Task<T> Remove(int Id);
        Task<T> Remove(Expression<Func<T, bool>> filter, string includeProperties = null);
        void RemoveRange(IEnumerable<T> entity);
        Task<bool> Save();
    }
}
