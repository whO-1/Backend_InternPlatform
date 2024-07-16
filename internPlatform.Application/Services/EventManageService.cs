using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class EventManageService : IEventManageService
    {

        private readonly IRepository<Event> _repository;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IEntityManageService<AgeGroup> _serviceAge;
        private readonly IEntityManageService<EntryType> _serviceEntry;
        public EventManageService(
            IRepository<Event> repository,
            IRepository<Category> repositoryCategory,
            IEntityManageService<AgeGroup> serviceAge,
            IEntityManageService<EntryType> serviceEntry
        )
        {
            _repository = repository;
            _serviceAge = serviceAge;
            _repositoryCategory = repositoryCategory;
            _serviceEntry = serviceEntry;
        }



        public async Task<Event> Get(Expression<Func<Event, bool>> filter, string includeProperties = null)
        {
            return await _repository.Get(filter, includeProperties);
        }

        public List<EventDTO> GetAllEvents(bool isSuperAdmin, string userName)
        {
            List<EventDTO> eventDTOList = new List<EventDTO>();
            List<Event> events;
            if (isSuperAdmin)
            {
                events = _repository.GetAll(null, includeProperties: "Entry,Age,Categories").ToList();
            }
            else
            {
                events = _repository.GetAll(null, includeProperties: "Entry,Age,Categories").Where(e => e.AuthorId == userName).ToList();
            }

            events.ForEach(e =>
            {
                eventDTOList.Add(new EventDTO
                {
                    Id = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    Age = e.Age,
                    Entry = e.Entry,
                    Categories = e.Categories.Select(c => c.Name).ToList(),
                    AuthorId = e.AuthorId,
                    SpecialGuests = e.SpecialGuests,
                    TimeStamp = e.TimeStamp,
                });
            });

            return eventDTOList;
        }

        public Event Add(Event entity)
        {
            return _repository.Add(entity);
        }

        public async Task<Event> Remove(int Id)
        {
            Event entity = await _repository.GetById(Id);
            if (entity != null)
            {
                return _repository.Remove(entity);
            }
            return null;
        }

        public async Task<Event> Remove(Expression<Func<Event, bool>> filter, string includeProperties = null)
        {
            Event entity = await _repository.Get(filter, includeProperties);
            if (entity != null)
            {
                return _repository.Remove(entity);
            }
            return null;
        }

        public void RemoveRange(IEnumerable<Event> entity)
        {
            _repository.RemoveRange(entity);
        }

        public void Update(Event entity)
        {
            entity.TimeStamp.PerformUpdate();
            _repository.Update(entity);
        }

        public async Task<bool> Save()
        {
            try
            {
                var result = await _repository.Save();
                return (result > 0) ? true : false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return false;
        }


        public async Task<EventUpSertViewModel> GetEventModel(string CurrentUser, int? Id = null)
        {
            var model = new EventUpSertViewModel();
            if (Id != null)
            {
                Event OldEvent = await Get(e => e.EventId == Id, includeProperties: "Entry,Age");
                if (OldEvent != null)
                {
                    model.Event = OldEvent;
                    model.SelectedCategories = OldEvent.Categories.Select(ec => ec.CategoryId).ToList();
                }
            }
            else
            {
                model.Event = new Event
                {
                    EventId = 0,
                    AuthorId = CurrentUser,
                };
            }

            PopulateWithEntities(model);
            return model;
        }

        //public  IEnumerable<Category> GetCategories(Event entity)
        //{

        //}



        public EventUpSertViewModel PopulateWithEntities(EventUpSertViewModel entity)
        {
            entity.Categories = _repositoryCategory.GetAll();
            entity.AgeGroups = _serviceAge.GetAll();
            entity.EntryTypes = _serviceEntry.GetAll();
            entity.SelectedCategories = (entity.SelectedCategories != null) ? entity.SelectedCategories : new List<int>();
            entity.Latitude = entity.Event.EventLocation.Latitude.ToString();
            entity.Longitude = entity.Event.EventLocation.Longitude.ToString();
            return entity;
        }

        public async Task<bool> EventUpSert(EventUpSertViewModel model)
        {

            if (model.Longitude != null && model.Latitude != null)
            {
                model.Event.EventLocation.Longitude = double.Parse(model.Longitude, CultureInfo.InvariantCulture);
                model.Event.EventLocation.Latitude = double.Parse(model.Latitude, CultureInfo.InvariantCulture);
            }
            Event getEvent = await _repository.Get(e => e.EventId == model.Event.EventId, "Categories");
            bool isNewEvent = (getEvent == null) ? true : false;
            var categories = (model.SelectedCategories != null) ? _repositoryCategory.GetAll(c => model.SelectedCategories.Contains(c.CategoryId)).ToList() : null;

            if (isNewEvent) { getEvent = new Event(); }
            getEvent.EventId = model.Event.EventId;
            getEvent.Title = model.Event.Title;
            getEvent.Description = model.Event.Description;
            getEvent.AuthorId = model.Event.AuthorId;
            getEvent.StartDate = model.Event.StartDate;
            getEvent.EndDate = model.Event.EndDate;
            getEvent.AgeGroupId = model.Event.AgeGroupId;
            getEvent.SpecialGuests = model.Event.SpecialGuests;
            getEvent.EventLocation = model.Event.EventLocation;
            getEvent.Categories = categories;
            getEvent.EntryTypeId = model.Event.EntryTypeId;

            if (isNewEvent)
            {
                Add(getEvent);
            }
            else
            {
                Update(getEvent);
            }

            bool result = await Save();
            return result;
        }


        public async Task<bool> RemoveEvent(int Id)
        {
            await Remove(Id);
            bool result = await Save();
            return result;

        }
    }
}
