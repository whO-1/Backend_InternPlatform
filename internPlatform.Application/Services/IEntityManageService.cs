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
        Task<T_DTO> Add(T_DTO entityDTO);
        Task<bool> Remove(int Id);
        Task<bool> Remove(Expression<Func<T, bool>> filter, string includeProperties = null);
        Task<bool> RemoveRange(IEnumerable<T> entity);
        Task<bool> Update(T entity);
        Task<bool> Save();
    }
}
