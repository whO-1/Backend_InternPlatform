using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using internPlatform.Domain.Models;
using internPlatform.Domain.Models.ViewModels;
using internPlatform.Infrastructure.Repository.IRepository;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internPlatform.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly IRepository<Link> _repositoryLinks;
        private readonly IRepository<Event> _repositoryEvents;

        public ApiService(IRepository<Link> repositoryLinks, IRepository<Event> repositoryEvents)
        {
            _repositoryLinks = repositoryLinks;
            _repositoryEvents = repositoryEvents;
        }

        public List<LinkDTO> GetLinks()
        {
            List<LinkDTO> linksCollection = new List<LinkDTO>();
            IEnumerable<Link> links = _repositoryLinks.GetAll();

            var en = links.GetEnumerator();
            while (en.MoveNext())
            {
                linksCollection.Add(new LinkDTO
                {
                    Id = en.Current.Id,
                    LinkTitle = en.Current.LinkTitle,
                    LinkUrl = en.Current.LinkUrl,
                    DisplayOrder = en.Current.DisplayOrder,
                    HeadId = en.Current.HeadId
                });
            }
            return linksCollection;
        }

        //public List<EventDTO> GetEvents()
        //{
        //    List<EventDTO> eventDTOList = new List<EventDTO>();
        //    IEnumerable<Event> events = _repositoryEvents.GetAll(includeProperties: "Entry,Age").ToList();
        //    var en = events.GetEnumerator();

        //    while (en.MoveNext())
        //    {
        //        eventDTOList.Add(new EventDTO
        //        {
        //            Id = en.Current.EventId,
        //            Title = en.Current.Title,
        //            Description = en.Current.Description,
        //            Age = en.Current.Age,
        //            Entry = en.Current.Entry,
        //            Categories = en.Current.Categories.Select(ec => ec.CategoryId).ToList(),
        //            AuthorId = en.Current.AuthorId,
        //            SpecialGuests = en.Current.SpecialGuests,
        //            TimeStamp = en.Current.TimeStamp,
        //            StartDate = en.Current.StartDate,
        //            EndDate = en.Current.EndDate,
        //        });
        //    }

        //    return eventDTOList;
        //}

        public async Task<ApiEventViewModel> GetEventById(int Id)
        {
            ApiEventViewModel eventDTO = new ApiEventViewModel();
            Event e = await _repositoryEvents.Get(el => el.EventId == Id, null);

            eventDTO = new ApiEventViewModel
            {
                Id = e.EventId,
                Title = e.Title,
                Description = e.Description,
                SpecialGuests = e.SpecialGuests,
                AgeGroupId = e.AgeGroupId,
                AgeGroupName = e.Age.Name,
                EntryTypeId = e.EntryTypeId,
                EntryTypeName = e.Entry.Name,
                SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                TimeStamp = e.TimeStamp,
                EventLocation = e.EventLocation,
                StartDate = e.StartDate,
                EndDate = e.EndDate,
            };
            return eventDTO;
        }

        public async Task<PaginatedList<ApiEventViewModel>> GetEventsPaginated(string body)
        {
            PaginationOptions options = JsonConvert.DeserializeObject<PaginationOptions>(body);
            if (options == null)
            {
                options = new PaginationOptions();
            }

            PaginatedList<Event> events = await _repositoryEvents.GetPaginatedAsync(options, x => x.EventId);
            if (events != null)
            {
                List<ApiEventViewModel> eventDTOs = events.Select(e => new ApiEventViewModel
                {
                    Id = e.EventId,
                    Title = e.Title,
                    Description = e.Description,
                    SpecialGuests = e.SpecialGuests,
                    AgeGroupId = e.AgeGroupId,
                    AgeGroupName = (e.Age != null) ? e.Age.Name : "",
                    EntryTypeId = e.EntryTypeId,
                    EntryTypeName = (e.Entry != null) ? e.Entry.Name : "",
                    SelectedCategories = e.Categories.Select(c => c.Name).ToList(),
                    TimeStamp = e.TimeStamp,
                    EventLocation = e.EventLocation,
                    StartDate = e.StartDate,
                    EndDate = e.EndDate,
                }).ToList();

                PaginatedList<ApiEventViewModel> paginatedEventDTOs = new PaginatedList<ApiEventViewModel>(eventDTOs, events.TotalCount, events.CurrentPage, options.PageSize);
                return paginatedEventDTOs;
            }

            return null;
        }

    }
}
