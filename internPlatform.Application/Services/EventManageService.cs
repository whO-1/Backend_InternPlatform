using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Constants;
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
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Event> _repository;
        private readonly IRepository<Category> _repositoryCategory;
        private readonly IEntityManageService<AgeGroup, AgeGroupDTO> _serviceAge;
        private readonly IEntityManageService<EntryType, EntryTypeDTO> _serviceEntry;
        private readonly IRepository<Image> _imageRepository;

        public EventManageService(
            IRepository<Event> repository,
            IRepository<Category> repositoryCategory,
            IEntityManageService<AgeGroup, AgeGroupDTO> serviceAge,
            IEntityManageService<EntryType, EntryTypeDTO> serviceEntry,
            IRepository<Image> imageRepository,
            IRepository<User> repositoryUser
        )
        {
            _repository = repository;
            _serviceAge = serviceAge;
            _repositoryCategory = repositoryCategory;
            _serviceEntry = serviceEntry;
            _imageRepository = imageRepository;
            _repositoryUser = repositoryUser;
        }


        public List<BrifEventViewModel> GetAll(bool isSuperAdmin, string userName)
        {
            List<BrifEventViewModel> eventDTOList = new List<BrifEventViewModel>();
            List<Event> events;
            if (isSuperAdmin)
            {
                events = _repository.GetAll(null, includeProperties: "Entry,Age,Categories").Where(e => e.IsDeleted != true).ToList();
            }
            else
            {
                events = _repository.GetAll(null, includeProperties: "Entry,Age,Categories").Where(e => e.IsDeleted != true).Where(e => e.Author == userName).ToList();
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
                    Author = e.Author,
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
                Event OldEvent = await _repository.Get(e => e.EventId == Id, includeProperties: "Entry,Age");
                if (OldEvent != null && OldEvent.IsDeleted != true)
                {
                    model = new EventUpSertViewModel
                    {
                        Id = OldEvent.EventId,
                        Title = OldEvent.Title,
                        Description = OldEvent.Description,
                        SpecialGuests = OldEvent.SpecialGuests,
                        AgeGroupId = OldEvent.AgeGroupId,
                        EntryTypeId = OldEvent.EntryTypeId,
                        AuthorId = OldEvent.Author,
                        StartDate = OldEvent.StartDate.ToString("yyyy-MM-ddTHH:mm"),
                        EndDate = OldEvent.EndDate.ToString("yyyy-MM-ddTHH:mm"),
                        SelectedCategories = OldEvent.Categories.Select(c => c.CategoryId).ToList(),
                        Longitude = OldEvent.EventLocation.Longitude.ToString(),
                        Latitude = OldEvent.EventLocation.Latitude.ToString(),
                        MainImageId = OldEvent.Images.Where(i => i.IsMain == true).Select(i => i.ImageId).FirstOrDefault().ToString(),
                        StoredImages = OldEvent.Images.Select(i =>
                        {
                            return (new FileViewModel
                            {
                                FileId = i.ImageId,
                                FileName = i.Name,
                                DisplayOrder = i.DisplayOrder,
                            });
                        }).OrderBy(f => f.DisplayOrder).ToList(),
                        StoredImagesIds = string.Join(",", OldEvent.Images.OrderBy(i => i.DisplayOrder).Select(i => i.ImageId.ToString()).ToList()),
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
            if (entity.MainImageId == null)
            {
                entity.MainImageId = "0";
            }
            return entity;
        }


        public async Task<bool> EventUpSert(EventUpSertViewModel model)
        {
            Event getEvent = await _repository.Get(e => e.EventId == model.Id, "Categories");
            bool isNewEvent = (getEvent == null) ? true : false;

            var categories = (model.SelectedCategories != null) ? _repositoryCategory.GetAll(c => model.SelectedCategories.Contains(c.CategoryId)).ToList() : null;
            var inputIds = (model.InputImages != null) ? model.InputImages.Split(',').Select(int.Parse).ToList() : new List<int>();
            var storedIds = (model.StoredImagesIds != null) ? model.StoredImagesIds.Split(',').Select(int.Parse).ToList() : new List<int>();
            if (storedIds.Count + inputIds.Count <= Constants.MaxFilesPerPost)
            {
                inputIds.ForEach(id => storedIds.Add(id));
            }
            if (!isNewEvent)
            {
                _imageRepository.GetAll(img => img.Event.EventId == getEvent.EventId && !storedIds.Contains(img.ImageId)).ToList().ForEach(el => _imageRepository.Remove(el));
            }


            bool changes = false;
            foreach (var item in storedIds)
            {
                var img = await _imageRepository.GetById(item);
                if (img != null)
                {
                    int index = storedIds.IndexOf(img.ImageId);
                    if (index >= 0 && index != img.ImageId)
                    {
                        changes = true;
                        img.DisplayOrder = index;
                        img.IsTemp = false;
                        img.IsMain = false;
                        if (img.ImageId == int.Parse(model.MainImageId))
                        {
                            img.IsMain = true;
                        }
                        _imageRepository.Update(img);
                    }
                }
            }
            if (changes)
            {
                await _imageRepository.Save();
            }

            if (isNewEvent) { getEvent = new Event(); }

            getEvent.EventId = model.Id;
            getEvent.Title = model.Title;
            getEvent.Description = model.Description;
            getEvent.Author = model.AuthorId;
            getEvent.StartDate = DateTime.ParseExact(model.StartDate, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            getEvent.EndDate = DateTime.ParseExact(model.EndDate, "yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
            getEvent.EntryTypeId = model.EntryTypeId;
            getEvent.AgeGroupId = model.AgeGroupId;
            getEvent.SpecialGuests = model.SpecialGuests;
            getEvent.Categories = categories;
            getEvent.Images = _imageRepository.GetAll(i => storedIds.Contains(i.ImageId)).OrderBy(i => i.DisplayOrder).ToList();
            getEvent.IsDeleted = false;
            getEvent.User = await _repositoryUser.Get(u => u.Email == model.AuthorId);

            if (model.Longitude != null)
            {
                getEvent.EventLocation.Longitude = double.Parse(model.Longitude, CultureInfo.InvariantCulture);
            }
            if (model.Latitude != null)
            {
                getEvent.EventLocation.Latitude = double.Parse(model.Latitude, CultureInfo.InvariantCulture);
            }



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
            var entity = await _repository.GetById(Id);
            entity.IsDeleted = true;
            //await Remove(Id);
            _repository.Update(entity);
            bool result = await Save();
            return result;

        }


        public List<string> GetRemovedImages(string list, int eventId)
        {
            var idsList = (list != null) ? list.Split(',').Select(int.Parse).ToList() : new List<int>();
            var imageNames = _imageRepository.GetAll(img => !idsList.Contains(img.ImageId) && img.Event.EventId == eventId).Select(img => img.Name).ToList();
            return imageNames;
        }

        public List<string> GetImageNamesFromModel(string list)
        {
            var idsList = (list != null) ? list.Split(',').Select(int.Parse).ToList() : new List<int>();
            var imageNames = _imageRepository.GetAll(img => idsList.Contains(img.ImageId)).Select(img => img.Name).ToList();
            return imageNames;
        }

        public async Task<PaginatedList<BrifEventViewModel>> GetPaginatedTableAsync(bool isSuperAdmin, string userName, int draw, int start, int length, string searchValue, int sortColumnIndex, string sortDirection)
        {
            IQueryable<Event> query = _repository.GetAll(null, includeProperties: "Entry,Age,Categories").Where(e => e.IsDeleted != true);
            if (!isSuperAdmin)
            {
                query = query.Where(e => e.Author == userName);
            }

            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(e =>
                    e.Title.Contains(searchValue) ||
                    e.Description.Contains(searchValue) ||
                    e.SpecialGuests.Contains(searchValue) ||
                    e.Age.Name.Contains(searchValue) ||
                    e.Entry.Name.Contains(searchValue) ||
                    e.Categories.Any(c => c.Name.Contains(searchValue))
                );
            }

            switch (sortColumnIndex)
            {
                case 0:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.EventId) : query.OrderByDescending(e => e.EventId);
                    break;
                case 1:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.Title) : query.OrderByDescending(e => e.Title);
                    break;
                case 2:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.Description) : query.OrderByDescending(e => e.Description);
                    break;
                case 3:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.TimeStamp.UpdateDate) : query.OrderByDescending(e => e.TimeStamp.UpdateDate);
                    break;
                case 4:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.StartDate) : query.OrderByDescending(e => e.StartDate);
                    break;
                default:
                    query = sortDirection == "asc" ? query.OrderBy(e => e.EventId) : query.OrderByDescending(e => e.EventId);
                    break;
            }

            var options = new PaginationOptions
            {
                PageSize = length,
                CurrentPage = (start / length) + 1
            };
            PaginatedList<Event> paginatedEvents = await _repository.GetPaginatedAsync(query, options);
            List<BrifEventViewModel> eventDTOList = paginatedEvents.Select(e => new BrifEventViewModel
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                SpecialGuests = e.SpecialGuests,
                AgeGroup = e.Age?.Name ?? "",
                EntryType = e.Entry?.Name ?? "",
                SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                Author = e.Author,
                LastUpdate = e.TimeStamp.UpdateDate,
                StartDate = e.StartDate,
            }).ToList();
            return new PaginatedList<BrifEventViewModel>(eventDTOList, paginatedEvents.TotalCount, paginatedEvents.CurrentPage, options.PageSize);
        }
    }
}
