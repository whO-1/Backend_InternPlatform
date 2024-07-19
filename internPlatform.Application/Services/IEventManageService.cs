using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public interface IEventManageService
    {
        Task<EventDTO> Get(Expression<Func<Event, bool>> filter, string includeProperties = null);
        List<BrifEventViewModel> GetAll(bool isAdmin, string userName);
        Event Add(Event entity);
        void Update(Event entity);
        Task<EventUpSertViewModel> GetEventModel(string AuthorId, int? Id = null);
        EventUpSertViewModel PopulateWithEntities(EventUpSertViewModel entity);
        Task<bool> EventUpSert(EventUpSertViewModel entity);
        Task<Event> Remove(int Id);
        Task<Event> Remove(Expression<Func<Event, bool>> filter, string includeProperties = null);
        void RemoveRange(IEnumerable<Event> entity);
        Task<bool> RemoveEvent(int Id);
        Task<bool> Save();
    }
}
