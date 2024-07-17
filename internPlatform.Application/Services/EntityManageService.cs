using internPlatform.Application.Services.Mappings;
using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class EntityManageService<T, T_DTO> : IEntityManageService<T, T_DTO>
        where T : class
        where T_DTO : class

    {
        private readonly IRepository<T> _repository;
        private readonly IBaseConvertor<T, T_DTO> _mapper;
        public EntityManageService(IRepository<T> repository, IBaseConvertor<T, T_DTO> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<T_DTO> Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            T entity = await _repository.Get(filter, includeProperties);
            return _mapper.EntityToDTO(entity);
        }

        public IEnumerable<T_DTO> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            List<T_DTO> DTOentities = new List<T_DTO>();
            IEnumerable<T> entities = _repository.GetAll(filter, includeProperties);
            var en = entities.GetEnumerator();
            if (en != null)
            {
                do
                {
                    DTOentities.Add(_mapper.EntityToDTO(en.Current));

                } while (en.MoveNext());
            }
            return DTOentities;
        }

        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public async Task<T> Remove(int Id)
        {
            T entity = await _repository.GetById(Id);
            if (entity != null)
            {
                return _repository.Remove(entity);
            }
            return null;
        }

        public async Task<T> Remove(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            T entity = await _repository.Get(filter, includeProperties);
            if (entity != null)
            {
                return _repository.Remove(entity);
            }
            return null;
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _repository.RemoveRange(entity);
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }
        public async Task<bool> Save()
        {
            var result = await _repository.Save();
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
