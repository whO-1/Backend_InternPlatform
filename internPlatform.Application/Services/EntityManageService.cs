using internPlatform.Domain.Entities;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace internPlatform.Application.Services
{
    public class EntityManageService<T> : IEntityManageService <T> where T : class
    {
        private readonly IRepository<T> _repository;
        public EntityManageService(IRepository<T> repository) 
        {
            _repository = repository;
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter, string includeProperties = null)
        {
            return await _repository.Get(filter, includeProperties);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null,string includeProperties = null)
        {
            return _repository.GetAll(filter,includeProperties);
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
            T entity =  await _repository.Get(filter, includeProperties);
            if(entity != null)
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
