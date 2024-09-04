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

        virtual public async Task<T_DTO> Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            T entity = await _repository.Get(filter, includeProperties);
            return _mapper.EntityToDTO(entity);
        }

        virtual public IEnumerable<T_DTO> GetAll(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            List<T_DTO> DTOentities = new List<T_DTO>();
            IEnumerable<T> entities = _repository.GetAll(filter, includeProperties);
            foreach (var item in entities)
            {
                DTOentities.Add(_mapper.EntityToDTO(item));
            }
            return DTOentities;
        }

        virtual public async Task<T_DTO> Add(T_DTO entityDTO)
        {
            T entity = _mapper.DTOToEntity(entityDTO);
            var result = _repository.Add(entity);
            await Save();
            return _mapper.EntityToDTO(result);
        }

        virtual public async Task<bool> Remove(int Id)
        {
            T entity = await _repository.GetById(Id);
            if (entity != null)
            {
                _repository.Remove(entity);
                return await Save();
            }
            return false;
        }

        virtual public async Task<bool> Remove(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            T entity = await _repository.Get(filter, includeProperties);
            if (entity != null)
            {
                _repository.Remove(entity);
                return await Save();
            }
            return false;
        }

        virtual public async Task<bool> RemoveRange(IEnumerable<T> entity)
        {
            _repository.RemoveRange(entity);
            return await Save();
        }

        virtual public async Task<bool> Update(T entity)
        {
            _repository.Update(entity);
            return await Save();
        }
        virtual public async Task<bool> Save()
        {
            int result = await _repository.Save();
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
