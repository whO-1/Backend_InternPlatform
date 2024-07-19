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
        private readonly IEntityManageService<AgeGroup, AgeGroupDTO> _serviceAge;
        private readonly IEntityManageService<EntryType, EntryTypeDTO> _serviceEntry;
        public EventManageService(
            IRepository<Event> repository,
            IRepository<Category> repositoryCategory,
            IEntityManageService<AgeGroup, AgeGroupDTO> serviceAge,
            IEntityManageService<EntryType, EntryTypeDTO> serviceEntry
        )
        {
            _repository = repository;
            _serviceAge = serviceAge;
            _repositoryCategory = repositoryCategory;
            _serviceEntry = serviceEntry;
        }



        public async Task<EventDTO> Get(Expression<Func<Event, bool>> filter, string includeProperties = null)
        {
            var e = await _repository.Get(filter, includeProperties);
            return new EventDTO
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                SpecialGuests = e.SpecialGuests,
                AgeGroupId = e.AgeGroupId ?? 0,
                EntryTypeId = e.EntryTypeId ?? 0,
                Categories = e.Categories.Select(c => c.CategoryId).ToList(),
                AuthorId = e.AuthorId,
                EventLocation = e.EventLocation,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
                TimeStamp = e.TimeStamp,
            };
        }
        public List<BrifEventViewModel> GetAll(bool isSuperAdmin, string userName)
        {
            List<BrifEventViewModel> eventDTOList = new List<BrifEventViewModel>();
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
                eventDTOList.Add(new BrifEventViewModel
                {
                    Id = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    SpecialGuests = e.SpecialGuests,
                    AgeGroup = (e.Age != null) ? e.Age.Name : "",
                    EntryType = (e.Entry != null) ? e.Entry.Name : "",
                    SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                    Author = e.AuthorId,
                    LastUpdate = e.TimeStamp.UpdateDate,
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
            EventUpSertViewModel model = new EventUpSertViewModel();
            model.AuthorId = CurrentUser;
            if (Id != null)
            {
                EventDTO OldEvent = await Get(e => e.EventId == Id, includeProperties: "Entry,Age");
                if (OldEvent != null)
                {
                    model = new EventUpSertViewModel
                    {
                        Id = OldEvent.Id,
                        Title = OldEvent.Title,
                        Description = OldEvent.Description,
                        SpecialGuests = OldEvent.SpecialGuests,
                        AgeGroupId = OldEvent.AgeGroupId,
                        EntryTypeId = OldEvent.EntryTypeId,
                        AuthorId = OldEvent.AuthorId,
                        StartDate = OldEvent.StartDate.ToString("yyyy-MM-ddTHH:mm"),
                        EndDate = OldEvent.EndDate.ToString("yyyy-MM-ddTHH:mm"),
                        SelectedCategories = OldEvent.Categories,
                        Longitude = OldEvent.EventLocation.Longitude.ToString(),
                        Latitude = OldEvent.EventLocation.Latitude.ToString(),
                    };
                }
            }

            PopulateWithEntities(model);
            return model;
        }


        public EventUpSertViewModel PopulateWithEntities(EventUpSertViewModel entity)
        {
            entity.Categories = _repositoryCategory.GetAll().Select(c => new CategoryDTO
            {
                Id = c.CategoryId,
                Name = c.Name,
                DisplayOrder = c.DisplayOrder,
            }).ToList();
            entity.AgeGroups = _serviceAge.GetAll();
            entity.EntryTypes = _serviceEntry.GetAll();
            return entity;
        }


        public async Task<bool> EventUpSert(EventUpSertViewModel model)
        {

            Event getEvent = await _repository.Get(e => e.EventId == model.Id, "Categories");
            bool isNewEvent = (getEvent == null) ? true : false;
            var categories = (model.SelectedCategories != null) ? _repositoryCategory.GetAll(c => model.SelectedCategories.Contains(c.CategoryId)).ToList() : null;

            if (isNewEvent) { getEvent = new Event(); }

            getEvent.EventId = model.Id;
            getEvent.Title = model.Title;
            getEvent.Description = model.Description;
            getEvent.AuthorId = model.AuthorId;
            getEvent.StartDate = DateTime.ParseExact(model.StartDate, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            getEvent.EndDate = DateTime.ParseExact(model.EndDate, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            getEvent.EntryTypeId = model.EntryTypeId;
            getEvent.AgeGroupId = model.AgeGroupId;
            getEvent.SpecialGuests = model.SpecialGuests;
            if (model.Longitude != null)
            {
                getEvent.EventLocation.Longitude = double.Parse(model.Longitude, CultureInfo.InvariantCulture);
            }
            if (model.Latitude != null)
            {
                getEvent.EventLocation.Latitude = double.Parse(model.Latitude, CultureInfo.InvariantCulture);
            }
            getEvent.Categories = categories;


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
