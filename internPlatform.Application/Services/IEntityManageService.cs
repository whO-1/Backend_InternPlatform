using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IEntityManageService<T, T_DTO>
        where T : class
        where T_DTO : class
    {
        Task<T_DTO> Get(Expression<Func<T, bool>> filter, string includeProperties = null);
        IEnumerable<T_DTO> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        T Add(T entity);
        void Update(T entity);

        Task<T> Remove(int Id);
        Task<T> Remove(Expression<Func<T, bool>> filter, string includeProperties = null);
        void RemoveRange(IEnumerable<T> entity);
        Task<bool> Save();
    }
}
